using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResXTools.Library.Builders.Base;
using ResXTools.Library.Builders.Interfaces;
using ResXTools.Library.Models;
using ResXTools.Library.Parsers;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ResXTools.Library.Builders.ServiceBuilders
{
    public class ServiceResourceAccessorBuilder : CodeBuilder
    {
        protected readonly List<Resource> _resources;

        public ServiceResourceAccessorBuilder(List<Resource> resources)
        {
            _resources = resources;
        }

        public List<MemberDeclarationSyntax> ResourceAccesors { get; private set; } = new List<MemberDeclarationSyntax>();

        public List<PropertyDeclarationSyntax> PropertyAccessors { get; private set; } = new List<PropertyDeclarationSyntax>();

        public List<MethodDeclarationSyntax> MethodAccessors { get; private set; } = new List<MethodDeclarationSyntax>();

        public override ICodeBuilder Build()
        {
            foreach (var resource in _resources)
            {
                var resourceContext = new ResourceParser(resource).Parse();

                if (resourceContext.IsFormatted)
                {
                    MethodAccessors.Add(GetFormattedStringAccessor(resourceContext, Identifiers.FormatMethod));
                }
                else if (resourceContext.IsReplaced)
                {
                    MethodAccessors.Add(GetFormattedStringAccessor(resourceContext, Identifiers.ReplaceMethod));
                }
                else if (resourceContext.IsEscaped)
                {
                    PropertyAccessors.Add(GetEscapedStringAccessor(resource));
                }
                else
                {
                    PropertyAccessors.Add(GetSimpleAccessor(resource));
                }
            }

            ResourceAccesors.AddRange(PropertyAccessors);
            ResourceAccesors.AddRange(MethodAccessors);

            return this;
        }

        public PropertyDeclarationSyntax GetSimpleAccessor(Resource resource)
        {
            return PropertyDeclaration(
                PredefinedType(
                    Token(SyntaxKind.StringKeyword)),
                Identifier(resource.Key))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithExpressionBody(
                ArrowExpressionClause(
                    ElementAccessExpression(
                        IdentifierName(Identifiers.LocalizerField))
                    .WithArgumentList(
                        BracketedArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName(Identifiers.KeysClass),
                                        IdentifierName(resource.Key))))))))
            .WithSemicolonToken(
                Token(SyntaxKind.SemicolonToken));
        }

        public PropertyDeclarationSyntax GetEscapedStringAccessor(Resource resource)
        {
            return PropertyDeclaration(
                PredefinedType(
                    Token(SyntaxKind.StringKeyword)),
                Identifier(resource.Key))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithExpressionBody(
                ArrowExpressionClause(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(Identifiers.ResourceFormatterClass),
                            IdentifierName(Identifiers.UnescapeMethod)))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    ElementAccessExpression(
                                        IdentifierName(Identifiers.LocalizerField))
                                    .WithArgumentList(
                                        BracketedArgumentList(
                                            SingletonSeparatedList<ArgumentSyntax>(
                                                Argument(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName(Identifiers.KeysClass),
                                                        IdentifierName(resource.Key))))))))))))
            .WithSemicolonToken(
                Token(SyntaxKind.SemicolonToken));
        }

        public MethodDeclarationSyntax GetFormattedStringAccessor(ResourceContext resourceContext, string formatterMethod)
        {
            return MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.StringKeyword)),
                Identifier(resourceContext.Resource.Key))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(GetMethodAccessorParameters(resourceContext.Tokens))
            .WithBody(
                Block(
                    SingletonList<StatementSyntax>(
                        ReturnStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName(Identifiers.ResourceFormatterClass),
                                    IdentifierName(formatterMethod)))
                            .WithArgumentList(
                                ArgumentList(
                                    SeparatedList<ArgumentSyntax>(
                                        new SyntaxNodeOrToken[]{
                                            Argument(
                                                ElementAccessExpression(
                                                    IdentifierName(Identifiers.LocalizerField))
                                                .WithArgumentList(
                                                    BracketedArgumentList(
                                                        SingletonSeparatedList<ArgumentSyntax>(
                                                            Argument(
                                                                MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    IdentifierName(Identifiers.KeysClass),
                                                                    IdentifierName(resourceContext.Resource.Key))))))),
                                            Token(SyntaxKind.CommaToken),
                                            Argument(
                                                ObjectCreationExpression(
                                                    GenericName(
                                                        Identifier("List"))
                                                    .WithTypeArgumentList(
                                                        TypeArgumentList(
                                                            SingletonSeparatedList<TypeSyntax>(
                                                                IdentifierName(Identifiers.FormatParameterClass)))))
                                                .WithInitializer(
                                                    InitializerExpression(
                                                        SyntaxKind.CollectionInitializerExpression,
                                                        GetFormatParameters(resourceContext.Tokens))))})))))));
        }

        private ParameterListSyntax GetMethodAccessorParameters(List<string> resourceTokens)
        {
            if (resourceTokens is null || !resourceTokens.Any())
            {
                return null;
            }

            // Add first parameter
            var parameterList = SingletonSeparatedList(
                Parameter(
                    Identifier(resourceTokens.First()))
                .WithType(
                    PredefinedType(
                        Token(SyntaxKind.ObjectKeyword))));

            if (resourceTokens.Count > 1)
            {
                // Add remaining parameters with commas
                var nodesAndTokens = new List<SyntaxNodeOrToken>();

                foreach (var token in resourceTokens)
                {
                    nodesAndTokens.Add(Token(SyntaxKind.CommaToken));

                    nodesAndTokens.Add(
                        Parameter(
                            Identifier(token))
                        .WithType(
                            PredefinedType(
                                Token(SyntaxKind.ObjectKeyword))));
                }

                parameterList.AddRange(SeparatedList<ParameterSyntax>(nodesAndTokens));
            }

            return ParameterList(parameterList);
        }

        private SeparatedSyntaxList<ExpressionSyntax> GetFormatParameters(List<string> resourceTokens)
        {
            var formatParameters = new List<SyntaxNodeOrToken>();

            foreach (var token in resourceTokens)
            {
                formatParameters.Add(
                    ObjectCreationExpression(
                        IdentifierName(Identifiers.FormatParameterClass))
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList<ArgumentSyntax>(
                                new SyntaxNodeOrToken[]{
                                    Argument(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal(token))),
                                    Token(SyntaxKind.CommaToken),
                                    Argument(
                                        IdentifierName(token))}))));

                formatParameters.Add(Token(SyntaxKind.CommaToken));
            }

            return SeparatedList<ExpressionSyntax>(formatParameters);
        }
    }
}