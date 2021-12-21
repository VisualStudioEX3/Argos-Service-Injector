using Argos.Framework.ServiceInjector.Tests.Classes;
using Argos.Framework.ServiceInjector.Tests.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Argos.Framework.ServiceInjector.Tests.TestClasses
{
    /// <summary>
    /// Methods for test the abstract <see cref="AbstractArgosServiceProvider"/> class.
    /// </summary>
    [TestClass]
    public class AbstractArgosServiceProviderTests
    {
        #region Tests
        [TestMethod]
        public void TestCustomServiceProviderClass()
        {
            IColorSelectorService service = CustomServiceProviderTestClass.ServiceProvider.GetService<IColorSelectorService>();
            string expected = string.Format(IColorSelectorService.MESSAGE_TEMPLATE, IColorStore.COLOR_BLUE);

            Assert.AreEqual(expected, service.GetSelectedColorMessage());
        } 
        #endregion
    }
}
