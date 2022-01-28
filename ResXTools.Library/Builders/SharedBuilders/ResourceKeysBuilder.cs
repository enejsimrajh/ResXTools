using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResXTools.Library.Builders.Base;
using ResXTools.Library.Builders.Interfaces;
using ResXTools.Library.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ResXTools.Library.Builders.SharedBuilders
{
    public class ResourceKeysBuilder : CodeBuilder
    {
        protected readonly List<Resource> _resources;

        public ResourceKeysBuilder(List<Resource> resources)
        {
            _resources = resources;
        }

        public ClassDeclarationSyntax KeysClass { get; private set; }

        public override ICodeBuilder Build()
        {
            KeysClass = ClassDeclaration(Identifiers.KeysClass)
                .WithModifiers(
                    TokenList(
                        new[]{
                            Token(SyntaxKind.PublicKeyword),
                            Token(SyntaxKind.StaticKeyword)}))
                .WithMembers(
                    List<MemberDeclarationSyntax>(GetResourceKeyFields()));

            return this;
        }

        private List<MemberDeclarationSyntax> GetResourceKeyFields()
        {
            var resourceKeyFields = new List<MemberDeclarationSyntax>();

            foreach (var resource in _resources)
            {
                resourceKeyFields.Add(GetResourceKeyField(resource.Key));
            }

            return resourceKeyFields;
        }

        private FieldDeclarationSyntax GetResourceKeyField(string key)
        {
            return
                FieldDeclaration(
                    VariableDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.StringKeyword)))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                Identifier(key))
                            .WithInitializer(
                                EqualsValueClause(
                                    LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Literal(key)))))))
                .WithModifiers(
                    TokenList(
                        new[]{
                            Token(SyntaxKind.PublicKeyword),
                            Token(SyntaxKind.StaticKeyword),
                            Token(SyntaxKind.ReadOnlyKeyword)}));
        }
    }
}