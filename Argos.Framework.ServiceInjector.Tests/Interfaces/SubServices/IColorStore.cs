namespace Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices
{
    public interface IColorStore
    {
        #region Constants
        public const string COLOR_RED = "Red";
        public const string COLOR_GREEN = "Green";
        public const string COLOR_BLUE = "Blue";
        #endregion

        #region Properties
        public string[] Colors { get; }
        #endregion
    }
}
