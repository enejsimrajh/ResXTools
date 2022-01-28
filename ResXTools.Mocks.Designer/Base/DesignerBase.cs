using Microsoft.Extensions.DependencyInjection;
using ResXTools.Mocks.Designer.Models;

namespace ResXTools.Mocks.Designer.Base
{
    internal abstract class DesignerBase<TDesigner, TDerived>
        where TDesigner : class
        where TDerived : DesignerBase<TDesigner, TDerived>
    {
        internal abstract List<Resource> Resources { get; }

        internal static IServiceCollection AddService(IServiceCollection services)
        {
            services.AddSingleton<TDesigner, TDesigner>();
            services.AddSingleton<TDerived, TDerived>();

            return services;
        }

        internal void PrintResources()
        {
            Console.WriteLine($"{typeof(TDesigner).Name} resources");

            foreach (var resource in Resources)
            {
                resource.Print();
            }

            Console.WriteLine();
        }
    }
}
