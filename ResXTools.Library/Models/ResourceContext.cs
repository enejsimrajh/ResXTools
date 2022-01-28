using System.Collections.Generic;
using System.Linq;

namespace ResXTools.Library.Models
{
    public class ResourceContext
    {
        public ResourceContext(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource { get; set; }

        public bool IsEscaped { get; set; }

        public bool IsReplaced => Tokens.Any();

        public bool IsFormatted => IsEscaped && IsReplaced;

        public List<string> Tokens { get; set; }
    }
}