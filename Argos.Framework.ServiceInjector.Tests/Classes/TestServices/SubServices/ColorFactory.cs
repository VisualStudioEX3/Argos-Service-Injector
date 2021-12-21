using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices.SubServices
{
    public class ColorFactory : IColorFactory
    {
        #region Internal vars
        private readonly IColorStore _colorStore;
        #endregion

        #region Constructor
        public ColorFactory(IColorStore colorStore) => this._colorStore = colorStore;
        #endregion

        #region Methods & Functions
        public string GetColor(int index) => this._colorStore.Colors[index];
        #endregion
    }
}
