using Argos.Framework.ServiceInjector.Tests.Interfaces;

namespace Argos.Framework.ServiceInjector.Tests.Classes
{
    public class GenericTestService<T> : IGenericTestService<T>
    {
        #region Properties
        public T Field { get; set; }
        #endregion
    }
}
