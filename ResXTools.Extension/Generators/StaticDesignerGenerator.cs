using ResXTools.Library.Builders;

namespace ResXTools.Extension.Generators
{
    internal class StaticDesignerGenerator : DesignerGeneratorBase<StaticDesignerBuilder>
    {
        public const string Name = "StaticResXCodeGen";

        public const string Description = "Static access resource designer generator.";
    }
}