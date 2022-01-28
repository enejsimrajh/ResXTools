using EnvDTE;

namespace ResXTools.Extension.Commands.ApplyGenerator.Manual
{
    internal class ApplyGeneratorCommand : ApplyGeneratorCommandBase
    {
        private static DTE _dte;

        protected override Task InitializeCompletedAsync()
        {
            _dte = Package.GetService<DTE, DTE>();

            return base.InitializeCompletedAsync();
        }

        protected override void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ProjectItem item = _dte.SelectedItems.Item(1).ProjectItem;

            if (item != null)
            {
                item.Properties.Item("CustomTool").Value = GeneratorName;
            }
        }
    }
}