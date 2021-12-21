using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices.SubServices
{
    public class StringFormatterService : IStringFormatterService
    {
        #region Methods & Functions
        public string FormatString(string template, params string[] values) => string.Format(template, values);
        #endregion
    }
}
