using Argos.Framework.ServiceInjector.Tests.Interfaces;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices
{
    public class TestServiceWithSubServices : ITestService
    {
        #region Constructor
        public TestServiceWithSubServices(ICounterService counterService, IStringGenericTestService stringService)
        {
        }
        #endregion
    }
}
