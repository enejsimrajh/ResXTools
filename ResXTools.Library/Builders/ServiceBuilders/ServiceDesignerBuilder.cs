using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResXTools.Library.Builders.Base;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ResXTools.Library.Builders.ServiceBuilders
{
    public class ServiceDesignerBuilder : DesignerBuilder
    {
        protected ServiceResourceAccessorBuilder _accessorBuilder;

        public ServiceDesignerBuilder(string resourceFile, string classNamespace) : base(resourceFile, classNamespace)
        {
            _accessorBuilder = new ServiceResourceAccessorBuilder(_resources);
        }

        protected override FieldDeclarationSyntax[] GetFields()
        {
            return new FieldDeclarationSyntax[]
            {
                FieldDeclaration(
                    VariableDeclaration(GetLocalizerType())
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                Identifier(Identifiers.LocalizerField)))))
                .WithModifiers(
                    TokenList(
                        new[]{
                            Token(SyntaxKind.PrivateKeyword),
                            Token(SyntaxKind.ReadOnlyKeyword)}))
            };
        }

        protected override ConstructorDeclarationSyntax GetConstructor()
        {
            return
                ConstructorDeclaration(
                    Identifier(_className))
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList<ParameterSyntax>(
                            Parameter(
                                Identifier(Identifiers.LocalizerVariable))
                            .WithType(GetLocalizerType()))))
                .WithBody(
                    Block(
                        SingletonList<StatementSyntax>(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName(Identifiers.LocalizerField),
                                    IdentifierName(Identifiers.LocalizerVariable))))));
        }

        protected override List<MemberDeclarationSyntax> GetResourceAccessors()
        {
            _accessorBuilder.Build();

            return _accessorBuilder.ResourceAccesors;
        }

        private TypeSyntax GetLocalizerType()
        {
            return
                GenericName(
                    Identifier("IStringLocalizer"))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SingletonSeparatedList<TypeSyntax>(
                            IdentifierName(_className))));
        }
    }
}