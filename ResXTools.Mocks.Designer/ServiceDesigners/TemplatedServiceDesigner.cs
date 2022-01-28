using ResXTools.Mocks.Designer.Base;
using ResXTools.Mocks.Designer.Models;
using ResXTools.Mocks.Designer.Properties.Resources;

namespace ResXTools.Mocks.Designer.ServiceDesigners
{
    internal class TemplatedServiceDesigner : DesignerBase<ServiceDesignerTemplate, TemplatedServiceDesigner>
    {
        private readonly ServiceDesignerTemplate _serviceDesigner;

        public TemplatedServiceDesigner(ServiceDesignerTemplate resources)
        {
            _serviceDesigner = resources;
        }

        internal override List<Resource> Resources => new()
        {
            new Resource(ServiceDesignerTemplate.Keys.WelcomeMessage, _serviceDesigner.WelcomeMessage),
            new Resource(ServiceDesignerTemplate.Keys.UserWelcomeMessage, _serviceDesigner.UserWelcomeMessage("Enej")),
            new Resource(ServiceDesignerTemplate.Keys.EscapeExample, _serviceDesigner.EscapeExample),
            new Resource(ServiceDesignerTemplate.Keys.FormatExample, _serviceDesigner.FormatExample("Enej")),
        };
    }
}
