using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Classes;
using Argos.Framework.ServiceInjector.Tests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Argos.Framework.ServiceInjector.Tests.TestClasses
{
    /// <summary>
    /// Methods for test the <see cref="ArgosServiceProviderFactory"/> class.
    /// </summary>
    [TestClass]
    public class ArgosServiceProviderFactoryTests
    {
        #region Tests
        [TestMethod("Create Service Container.")]
        public void CreateServiceContainer()
        {
            ArgosServiceProviderFactory.CreateServiceContainer();
        }

        [TestMethod("Create Service Container, with register services action.")]
        public void CreateServiceContainerAnRegisterServices()
        {
            ArgosServiceProviderFactory.CreateServiceContainer((serviceContainer) => 
            {
                serviceContainer.AddService<ITestService, TestService>();
                serviceContainer.AddService<IStringGenericTestService, StringGenericTestService>();
                serviceContainer.AddService(typeof(IGenericTestService<>), typeof(GenericTestService<>));
            });
        }

        [TestMethod("Setup a custom IArgosServiceContainer implementation and try to create an instance.")]
        public void CreateCustomServiceContainer()
        {
            ArgosServiceProviderFactory.SetCustomServiceContainerImplementation<CustomArgosServiceContainerTestClass>();

            Assert.IsInstanceOfType(ArgosServiceProviderFactory.CreateServiceContainer(), typeof(CustomArgosServiceContainerTestClass));
            Assert.IsInstanceOfType(ArgosServiceProviderFactory.CreateServiceContainer(), typeof(IArgosServiceContainer));
        }
        #endregion
    }
}
