namespace ResXTools.Mocks.Designer.Properties.Resources
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Localization;

    public sealed class ServiceDesigner
    {
        private readonly IStringLocalizer<ServiceDesigner> _localizer;

        public ServiceDesigner(IStringLocalizer<ServiceDesigner> localizer)
        {
            _localizer = localizer;
        }

        public string WelcomeMessage => _localizer[Keys.WelcomeMessage];

        public string EscapeExample => ResourceFormatter.Unescape(_localizer[Keys.EscapeExample]);

        public string UserWelcomeMessage(object user)
        {
            return ResourceFormatter.Replace(
                _localizer[Keys.UserWelcomeMessage],
                new List<FormatParameter>
                {
                    new FormatParameter("user", user),
                });
        }

        public string FormatExample(object user)
        {
            return ResourceFormatter.Format(
                _localizer[Keys.FormatExample],
                new List<FormatParameter>
                {
                    new FormatParameter("user", user),
                });
        }

        public static class Keys
        {
            public static readonly string EscapeExample = "EscapeExample";
            public static readonly string FormatExample = "FormatExample";
            public static readonly string UserWelcomeMessage = "UserWelcomeMessage";
            public static readonly string WelcomeMessage = "WelcomeMessage";
        }

        private static class ResourceFormatter
        {
            private const char OpeningTokenChar = '{';
            private const char ClosingTokenChar = '}';

            public static string Format(string value, List<FormatParameter> parameters)
            {
                return Replace(Unescape(value), parameters);
            }

            public static string Replace(string value, List<FormatParameter> parameters)
            {
                return parameters
                    .Aggregate(new StringBuilder(value), (current, parameter) =>
                        current.Replace($"{{{parameter.Name}}}", parameter.ValueString))
                    .ToString();
            }

            public static string Unescape(string value)
            {
                var unescapedValue = new StringBuilder(value.Length);
                for (int index = 0, length = value.Length; index < length; index++)
                {
                    char @char = value[index];
                    if (@char == OpeningTokenChar || @char == ClosingTokenChar)
                    {
                        int nextIndex = index + 1;
                        if (nextIndex < length && value[nextIndex] == @char)
                        {
                            unescapedValue.Append(value[nextIndex]);
                            index++;
                            continue;
                        }
                    }

                    unescapedValue.Append(@char);
                }

                return unescapedValue.ToString();
            }
        }

        private class FormatParameter
        {
            public FormatParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public object Value { get; set; }

            public string ValueString => Value != null ? Value.ToString() : string.Empty;
        }
    }
}