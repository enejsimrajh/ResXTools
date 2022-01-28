using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using ResXTools.Library.Models;

namespace ResXTools.Library.Parsers
{
    internal class ResourceParser
    {
        private const char OpeningTokenChar = '{';
        private const char ClosingTokenChar = '}';

        private readonly Resource resource;

        public ResourceParser(Resource resource)
        {
            this.resource = resource;
        }

        public ResourceContext Parse()
        {
            var (tokens, isEscaped) = FindTokens();

            return new ResourceContext(resource)
            {
                IsEscaped = isEscaped,
                Tokens = tokens
            };
        }

        private (List<string> tokens, bool isEscaped) FindTokens()
        {
            (List<string> tokens, bool isEscaped) result = (new List<string>(), false);
            string resourceValue = resource.Value;

            if (string.IsNullOrWhiteSpace(resourceValue))
            {
                return result;
            }

            StringBuilder currentToken = null;

            for (int index = 0, length = resourceValue.Length; index < length; index++)
            {
                char @char = resourceValue[index];

                if (currentToken == null)
                {
                    // Token is null, that means we're parsing a string literal.
                    if (@char == OpeningTokenChar)
                    {
                        int nextIndex = index + 1;
                        if (nextIndex < length && resourceValue[nextIndex] == OpeningTokenChar)
                        {
                            result.isEscaped = true;
                            index++;
                            continue;
                        }

                        currentToken = new StringBuilder();
                    }
                }
                else
                {
                    // Token is not null, that means we're parsing a token name.
                    if (@char == ClosingTokenChar)
                    {
                        string currentTokenString = currentToken.ToString();

                        if (!result.tokens.Contains(currentTokenString) && SyntaxFacts.IsValidIdentifier(currentTokenString))
                        {
                            result.tokens.Add(currentTokenString);
                        }

                        currentToken = null;
                        continue;
                    }

                    currentToken.Append(@char);
                }
            }

            return result;
        }
    }
}