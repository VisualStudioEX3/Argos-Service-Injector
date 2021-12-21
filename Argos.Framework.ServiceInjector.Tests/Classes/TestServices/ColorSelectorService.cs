using Argos.Framework.ServiceInjector.Tests.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;

namespace Argos.Framework.ServiceInjector.Tests.Classes.TestServices
{
    public class ColorSelectorService : IColorSelectorService
    {
        #region Internal vars
        private readonly IStringFormatterService _stringFormatterService;
        private readonly IColorFactory _colorFactory;
        #endregion

        #region Constructor
        public ColorSelectorService(IColorFactory colorFactory, IStringFormatterService stringFormatterService)
        {
            this._colorFactory = colorFactory;
            this._stringFormatterService = stringFormatterService;
        }
        #endregion

        #region Methods & Functions
        public string GetSelectedColorMessage()
        {
            string color = this._colorFactory.GetColor(IColorSelectorService.SELECTED_COLOR_INDEX);
            string result = this._stringFormatterService.FormatString(IColorSelectorService.MESSAGE_TEMPLATE, color);

            return result;
        }
        #endregion
    }
}
