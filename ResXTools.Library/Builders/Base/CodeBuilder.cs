using ResXTools.Library.Builders.Interfaces;

namespace ResXTools.Library.Builders.Base
{
    public abstract class CodeBuilder : ICodeBuilder
    {
        public abstract ICodeBuilder Build();
    }
}