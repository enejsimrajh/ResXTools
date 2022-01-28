using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResXTools.Extension.Experiments.Properties.Resources;

namespace ResXTools.Extension.Experiments
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services
                .AddSingleton<Startup, Startup>()
                .BuildServiceProvider()
                .GetRequiredService<Startup>()
                .Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(configure => configure.AddConsole())
                .AddLocalization()
                .AddSingleton<ServiceDesigner, ServiceDesigner>();
        }
    }
}