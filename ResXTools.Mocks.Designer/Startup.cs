using Microsoft.Extensions.Logging;
using ResXTools.Mocks.Designer.Properties.Resources;

namespace ResXTools.Mocks.Designer
{
    internal class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly ServiceDesigner _serviceDesigner;

        public Startup(ILogger<Startup> logger, ServiceDesigner globalResources)
        {
            _logger = logger;
            _serviceDesigner = globalResources;
        }

        public void Run()
        {
            bool run;

            do
            {
                Execute();
                Thread.Sleep(10);
                Console.WriteLine("Press enter to run again, or any other key to exit the application ...");
                var keyPressed = Console.ReadKey(true);
                run = keyPressed.Key == ConsoleKey.Enter;
            }
            while (run);
        }

        private void Execute()
        {
            var resources = new List<Resource>
            {
                new Resource(ServiceDesigner.Keys.WelcomeMessage, _serviceDesigner.WelcomeMessage),
                new Resource(ServiceDesigner.Keys.UserWelcomeMessage, _serviceDesigner.UserWelcomeMessage("Enej")),
                new Resource(ServiceDesigner.Keys.EscapeExample, _serviceDesigner.EscapeExample),
                new Resource(ServiceDesigner.Keys.FormatExample, _serviceDesigner.FormatExample("Enej")),
            };

            foreach (var resource in resources)
            {
                resource.Print();
            }
        }

        private struct Resource
        {
            public Resource()
            {
            }

            public Resource(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; set; } = string.Empty;

            public string Value { get; set; } = string.Empty;

            public void Print()
            {
                Console.WriteLine($"Key: {Key}");
                Console.WriteLine($"Value: {Value}");
            }
        }
    }
}