using System.Collections.Generic;

namespace ResXTools.Library.Models
{
    public struct Resource
    {
        public Resource(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Resource(KeyValuePair<string, string> resource)
        {
            Key = resource.Key;
            Value = resource.Value;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }
    }
}