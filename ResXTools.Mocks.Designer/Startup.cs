using ResXTools.Mocks.Designer.Properties.Resources;
using ResXTools.Mocks.Designer.ServiceDesigners;

namespace ResXTools.Mocks.Designer
{
    internal class Startup
    {
        private readonly GeneratedServiceDesigner _generatedServiceDesigner;
        private readonly TemplatedServiceDesigner _templatedServiceDesigner;

        public Startup(GeneratedServiceDesigner generatedServiceDesigner, TemplatedServiceDesigner templatedServiceDesigner)
        {
            _generatedServiceDesigner = generatedServiceDesigner;
            _templatedServiceDesigner = templatedServiceDesigner;

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
            _generatedServiceDesigner.PrintResources();
            _templatedServiceDesigner.PrintResources();
        }
    }
}