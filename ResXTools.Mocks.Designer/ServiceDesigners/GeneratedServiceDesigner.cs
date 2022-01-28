using ResXTools.Mocks.Designer.Base;
using ResXTools.Mocks.Designer.Models;
using ResXTools.Mocks.Designer.Properties.Resources;

namespace ResXTools.Mocks.Designer.ServiceDesigners
{
    internal class GeneratedServiceDesigner : DesignerBase<ServiceDesigner, GeneratedServiceDesigner>
    {
        private readonly ServiceDesigner _serviceDesigner;

        public GeneratedServiceDesigner(ServiceDesigner resources)
        {
            _serviceDesigner = resources;
        }

        internal override List<Resource> Resources => new()
        {
            new Resource(ServiceDesigner.Keys.WelcomeMessage, _serviceDesigner.WelcomeMessage),
            new Resource(ServiceDesigner.Keys.UserWelcomeMessage, _serviceDesigner.UserWelcomeMessage("Enej")),
            new Resource(ServiceDesigner.Keys.EscapeExample, _serviceDesigner.EscapeExample),
            new Resource(ServiceDesigner.Keys.FormatExample, _serviceDesigner.FormatExample("Enej")),
        };
    }
}
