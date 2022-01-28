namespace ResXTools.Extension.Commands
{
    internal class ApplyGeneratorCommandBase : BaseCommand<ApplyGeneratorCommandBase>
    {
        protected virtual string GeneratorName { get; }
    }
}