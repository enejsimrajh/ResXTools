using ResXTools.Extension.Generators;

namespace ResXTools.Extension.Commands.ApplyGenerator.Manual
{
    [Command(PackageIds.ApplyStaticGeneratorCommand)]
    internal sealed class ApplyStaticGenerator : ApplyGeneratorCommand
    {
        protected override string GeneratorName => StaticDesignerGenerator.Name;
    }
}