global using System;
global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using ResXTools.Extension.Generators;

namespace ResXTools.Extension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.ResXToolsString)]
    [ProvideCodeGenerator(typeof(ServiceDesignerGenerator),
                          ServiceDesignerGenerator.Name,
                          ServiceDesignerGenerator.Description,
                          true,
                          RegisterCodeBase = true)]
    [ProvideCodeGeneratorExtension(ServiceDesignerGenerator.Name, ".resx")]
    [ProvideUIContextRule(PackageGuids.UIContextString,
                          name: "UI Context",
                          expression: "Resx",
                          termNames: new[] { "Resx" },
                          termValues: new[] { "HierSingleSelectionName:.resx$" })]
    public sealed class ResXCodeGenPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
        }
    }
}