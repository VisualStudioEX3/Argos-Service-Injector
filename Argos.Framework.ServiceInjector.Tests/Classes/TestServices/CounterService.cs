using Argos.Framework.ServiceInjector.Tests.Interfaces;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices
{
    public class CounterService : ICounterService
    {
        #region Properties
        public int Counter { get; set; }
        #endregion
    }
}
