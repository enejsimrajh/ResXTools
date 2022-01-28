using ResXTools.Library.Builders.Interfaces;
using ResXTools.Library.Extensions;

namespace ResXTools.Library.Builders.Factories
{
    public sealed class DesignerBuilderFactory
    {
        private static readonly DesignerBuilderFactory instance = new DesignerBuilderFactory();

        private DesignerBuilderFactory()
        { }

        public static DesignerBuilderFactory GetFactory()
        {
            return instance;
        }

        public T CreateBuilder<T>(string resourceFile, string classNamespace) where T : IDesignerBuilder
        {
            return ActivatorExtensions.CreateInstance<T>(new object[] { resourceFile, classNamespace });
        }
    }
}