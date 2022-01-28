﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResXTools.Mocks.Designer.Properties.Resources;

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
                .AddLocalization()
                .AddSingleton<ServiceDesigner, ServiceDesigner>();
        }
    }
}