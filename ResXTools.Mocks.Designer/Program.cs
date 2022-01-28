using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ResXTools.Mocks.Designer
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
                .AddLocalization();

            ServiceDesigners.GeneratedServiceDesigner.AddService(services);
            ServiceDesigners.TemplatedServiceDesigner.AddService(services);
        }
    }
}