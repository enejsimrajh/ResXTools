namespace ResXTools.Extension.Commands.ApplyGenerator.Auto
{
    internal class AutoApplyGeneratorCommand : ApplyGeneratorCommandBase
    {
        protected override Task InitializeCompletedAsync()
        {
            Command.Supported = false;
            return base.InitializeCompletedAsync();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            if (await VS.Solutions.GetActiveItemAsync() is PhysicalFile file)
            {
                await file.TrySetAttributeAsync("custom tool", GeneratorName);
            }
        }
    }
}