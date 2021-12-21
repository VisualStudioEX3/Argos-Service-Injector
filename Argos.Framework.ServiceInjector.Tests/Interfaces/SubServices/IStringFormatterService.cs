namespace Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices
{
    public interface IStringFormatterService
    {
        #region Methods & Functions
        string FormatString(string template, params string[] values);
        #endregion
    }
}
