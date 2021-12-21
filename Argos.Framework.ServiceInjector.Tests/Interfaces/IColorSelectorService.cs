namespace Argos.Framework.ServiceInjector.Tests.Interfaces
{
    public interface IColorSelectorService
    {
        #region Constants
        public const int SELECTED_COLOR_INDEX = 2;
        public const string MESSAGE_TEMPLATE = "The selected color is {0}.";
        #endregion

        #region Methods & Functions
        string GetSelectedColorMessage();
        #endregion
    }
}
