using ResXTools.Extension.Generators;

namespace ResXTools.Extension.Commands.ApplyGenerator.Manual
{
    [Command(PackageIds.ApplyServiceGeneratorCommand)]
    internal sealed class ApplyServiceGenerator : ApplyGeneratorCommand
    {
        protected override string GeneratorName => ServiceDesignerGenerator.Name;
    }
}