using Argos.Framework.ServiceInjector.Tests.Interfaces;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices
{
    public class TestServiceWithGenericSubService : ITestService
    {
        #region Constructor
        public TestServiceWithGenericSubService(IGenericTestService<string> genericService)
        {
        }
        #endregion
    }
}
