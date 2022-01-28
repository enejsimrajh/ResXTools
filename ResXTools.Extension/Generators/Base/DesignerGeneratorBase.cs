using System.Text;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using ResXTools.Library.Builders.Factories;
using ResXTools.Library.Builders.Interfaces;

namespace ResXTools.Extension.Generators
{
    internal abstract class DesignerGeneratorBase<TBuilder> : BaseCodeGeneratorWithSite
        where TBuilder : IDesignerBuilder
    {
        public override string GetDefaultExtension() => ".Designer.cs";

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var classBuilderFactory = DesignerBuilderFactory.GetFactory();
            var designerBuilder = classBuilderFactory.CreateBuilder<TBuilder>(inputFileName, this.FileNamespace);
            return Encoding.UTF8.GetBytes(designerBuilder.Build().ToString());
        }
    }
}