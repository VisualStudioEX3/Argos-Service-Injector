using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices.SubServices
{
    public class ColorStore : IColorStore
    {
        #region Properties
        public string[] Colors { get; }
        #endregion

        #region Constructor
        public ColorStore() => this.Colors = new string[] { IColorStore.COLOR_RED, IColorStore.COLOR_GREEN, IColorStore.COLOR_BLUE };
        #endregion
    }
}
