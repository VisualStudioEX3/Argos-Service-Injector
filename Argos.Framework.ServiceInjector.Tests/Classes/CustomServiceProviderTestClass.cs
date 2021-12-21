using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Classes.TestServices;
using Argos.Framework.ServiceInjector.Tests.Classes.TestServices.SubServices;
using Argos.Framework.ServiceInjector.Tests.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;

namespace Argos.Framework.ServiceInjector.Tests.Classes
{
    public class CustomServiceProviderTestClass : AbstractArgosServiceProvider<CustomServiceProviderTestClass>
    {
        #region Methods & Functions
        public override void RegisterServices(IArgosServiceContainer serviceContainer)
        {
            serviceContainer.AddService<IColorStore, ColorStore>();
            serviceContainer.AddService<IColorFactory, ColorFactory>();
            serviceContainer.AddService<IStringFormatterService, StringFormatterService>();
            serviceContainer.AddService<IColorSelectorService, ColorSelectorService>();
        }
        #endregion
    }
}
