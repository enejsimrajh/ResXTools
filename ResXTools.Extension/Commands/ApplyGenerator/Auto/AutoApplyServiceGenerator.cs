using ResXTools.Extension.Generators;

namespace ResXTools.Extension.Commands.ApplyGenerator.Auto
{
    //[Command(PackageIds.MyCommand)]
    internal sealed class AutoApplyServiceGenerator : AutoApplyGeneratorCommand
    {
        protected override string GeneratorName => ServiceDesignerGenerator.Name;
    }
}