using System;

namespace ResXTools.Library.Builders.Base
{
    public abstract class ClassBuilder : CodeBuilder
    {
        protected readonly string _className;
        protected readonly string _classNamespace;

        public ClassBuilder(string className, string classNamespace)
        {
            if (className is null)
                throw new ArgumentNullException(nameof(className));

            if (string.IsNullOrEmpty(classNamespace))
                throw new ArgumentNullException(nameof(classNamespace));

            _className = className;
            _classNamespace = classNamespace;
        }
    }
}