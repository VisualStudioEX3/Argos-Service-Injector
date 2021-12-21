namespace Argos.Framework.ServiceInjector.Tests.Interfaces
{
    public interface IGenericTestService<T>
    {
        #region Properties
        T Field { get; set; }
        #endregion
    }
}
