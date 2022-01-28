﻿using ResXTools.Extension.Generators;

namespace ResXTools.Extension.Commands.ApplyGenerator.Auto
{
    //[Command(PackageIds.MyCommand)]
    internal sealed class AutoApplyStaticGenerator : AutoApplyGeneratorCommand
    {
        protected override string GeneratorName => StaticDesignerGenerator.Name;
    }
}