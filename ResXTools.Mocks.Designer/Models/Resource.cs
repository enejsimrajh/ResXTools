using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResXTools.Mocks.Designer.Models
{
    internal struct Resource
    {
        public Resource()
        {
        }

        public Resource(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public void Print()
        {
            Console.WriteLine($"Key: {Key}");
            Console.WriteLine($"Value: {Value}");
        }
    }
}
