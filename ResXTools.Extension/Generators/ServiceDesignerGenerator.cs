using ResXTools.Library.Builders.ServiceBuilders;

namespace ResXTools.Extension.Generators
{
    internal class ServiceDesignerGenerator : DesignerGeneratorBase<ServiceDesignerBuilder>
    {
        public const string Name = "ServiceResXCodeGen";

        public const string Description = "Injected service resource designer generator.";
    }
}