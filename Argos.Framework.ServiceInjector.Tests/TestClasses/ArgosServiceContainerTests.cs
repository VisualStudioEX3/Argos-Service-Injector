using Argos.Framework.ServiceInjector.Contracts.Exceptions;
using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Classes;
using Argos.Framework.ServiceInjector.Tests.Classes.TestServices;
using Argos.Framework.ServiceInjector.Tests.Classes.TestServices.SubServices;
using Argos.Framework.ServiceInjector.Tests.Interfaces;
using Argos.Framework.ServiceInjector.Tests.Interfaces.SubServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Argos.Framework.ServiceInjector.Tests.TestClasses
{
    /// <summary>
    /// Methods for test the internal ArgosServiceContainer class.
    /// </summary>
    [TestClass]
    public class ArgosServiceContainerTests
    {
        #region Internal vars
        private IArgosServiceContainer _serviceContainer;
        #endregion

        #region Initializers
        [TestInitialize]
        public void CreateServiceContainer() => this._serviceContainer = ArgosServiceProviderFactory.CreateServiceContainer();
        #endregion

        #region Methods & Functions
        private void RegisterITestService() => this._serviceContainer.AddService<ITestService, TestService>();

        private void RegisterAndTestIColorSelectorServiceImplementation()
        {
            this._serviceContainer.AddService<IColorSelectorService, ColorSelectorService>();

            IColorSelectorService service = this._serviceContainer.GetService<IColorSelectorService>();
            string expected = string.Format(IColorSelectorService.MESSAGE_TEMPLATE, IColorStore.COLOR_BLUE);

            Assert.AreEqual(expected, service.GetSelectedColorMessage());
        }
        #endregion

        #region Tests
        [DataTestMethod]
        [DataRow(typeof(ITestService), typeof(TestService), DisplayName = "Register TestService as ITestService.")]
        [DataRow(typeof(IStringGenericTestService), typeof(StringGenericTestService), DisplayName = "Register StringGenericService as IStringGenericService.")]
        [DataRow(typeof(IGenericTestService<>), typeof(GenericTestService<>), DisplayName = "Register GenericService<> as IGenericService<>.")]
        public void RegisterService(Type template, Type implementation) => this._serviceContainer.AddService(template, implementation);

        [DataTestMethod]
        [DataRow(typeof(ITestService), typeof(StringGenericTestService), DisplayName = "Fail register StringGenericTestService as ITestService.")]
        [DataRow(typeof(IStringGenericTestService), typeof(GenericTestService<string>), DisplayName = "Fail register GenericTestService<string> as IStringGenericTestService.")]
        [DataRow(typeof(IGenericTestService<>), typeof(GenericTestService<int>), DisplayName = "Fail register GenericTestService<int> as IGenericTestService<>.")]
        public void FailRegisterServiceWithWrongInterface(Type template, Type implementation)
        {
            Assert.ThrowsException<ServiceNotImplementsInterfaceException>(() => this._serviceContainer.AddService(template, implementation));
        }

        [TestMethod("Fail register abstract service.")]
        public void FailRegisterAbstractService()
        {
            Assert.ThrowsException<ServiceAbstractClassException>(() => this._serviceContainer.AddService<ITestService, AbstractTestService>());
        }

        [TestMethod("Try to register twice a service.")]
        public void TryToRegisterTwiceAService()
        {
            this.RegisterITestService();
            Assert.ThrowsException<ServiceAlreadyRegisteredException>(() => this.RegisterITestService());
        }

        [TestMethod("Try to register a service using class instead of a interface as key.")]
        public void TryToRegisterServiceUsingClass()
        {
            Assert.ThrowsException<InvalidServiceInterfaceException>(() => this._serviceContainer.AddService<TestService, TestService>());
        }

        [TestMethod("Try to register a service using an interface instead a class as implementation.")]
        public void TryToRegisterInterfaceAsService()
        {
            Assert.ThrowsException<InvalidServiceClassException>(() => this._serviceContainer.AddService<ITestService, ITestService>());
        }

        [DataTestMethod]
        [DataRow(typeof(ITestService), typeof(TestService), DisplayName = "Remove ITestService.")]
        [DataRow(typeof(IGenericTestService<>), typeof(GenericTestService<>), DisplayName = "Remove IGenericTestService<>.")]
        public void RemoveService(Type template, Type implementation)
        {
            this._serviceContainer.AddService(template, implementation);
            this._serviceContainer.RemoveService(template);
        }

        [TestMethod("Fail removing a service that not are previously registered.")]
        public void FailRemoveService() => Assert.ThrowsException<ServiceNotFoundException>(() => this._serviceContainer.RemoveService(typeof(ITestService)));

        [DataTestMethod]
        [DataRow(typeof(ITestService), typeof(TestService), DisplayName = "Get ITestService with TestService implementation.")]
        [DataRow(typeof(IStringGenericTestService), typeof(StringGenericTestService), DisplayName = "Get IStringGenericTestService with StringGenericTestService implementation.")]
        public void GetNonGenericService(Type template, Type implementation)
        {
            this._serviceContainer.AddService(template, implementation);
            this._serviceContainer.GetService(template);
        }

        [TestMethod("Get generic service.")]
        public void GetGenericService()
        {
            const string STRING_VALUE = "Hello, World!";

            this._serviceContainer.AddService(typeof(IGenericTestService<>), typeof(GenericTestService<>));
            var service = (IGenericTestService<string>)this._serviceContainer.GetGenericService(typeof(IGenericTestService<>), typeof(string));

            service.Field = STRING_VALUE;

            Assert.AreEqual(STRING_VALUE, service.Field);
        }

        [TestMethod("Register and get a singleton service.")]
        public void RegisterAndGetSingletonService()
        {
            const int LOOP = 5;

            ICounterService service = null;

            this._serviceContainer.AddSingleton<ICounterService, CounterService>();

            for (int i = 0; i < LOOP; i++)
            {
                service = this._serviceContainer.GetService<ICounterService>();
                service.Counter++;
            }

            Assert.AreEqual(LOOP, service.Counter);
        }

        [TestMethod("Fail get service that are not previously registered.")]
        public void FailGetService() => Assert.ThrowsException<ServiceNotFoundException>(() => this._serviceContainer.GetService<ITestService>());

        [TestMethod("Get service that request sub services in his constructor (as parameters).")]
        public void GetServiceWithSubServices()
        {
            this._serviceContainer.AddService<IColorStore, ColorStore>();
            this._serviceContainer.AddService<IColorFactory, ColorFactory>();
            this._serviceContainer.AddService<IStringFormatterService, StringFormatterService>();

            this.RegisterAndTestIColorSelectorServiceImplementation();
        }
        
        [TestMethod("Get service that request sub services in his constructor (as parameters) from different service containers.")]
        public void GetServiceWithSubServicesFromDifferentServiceContainers()
        {
            IArgosServiceContainer colorServicesContainer = ArgosServiceProviderFactory.CreateServiceContainer((serviceContainer) => 
            {
                serviceContainer.AddService<IColorStore, ColorStore>();
                serviceContainer.AddService<IColorFactory, ColorFactory>();
            });

            IArgosServiceContainer helperServicesContainer = ArgosServiceProviderFactory.CreateServiceContainer((serviceContainer) =>
            {
                serviceContainer.AddService<IStringFormatterService, StringFormatterService>();
            });

            this._serviceContainer.AddServiceProviders(colorServicesContainer, helperServicesContainer);

            this.RegisterAndTestIColorSelectorServiceImplementation();
        }

        [TestMethod("Get service with generic subservice in his constructor (as parameters).")]
        public void GetServiceWithGenericSubService()
        {
            this._serviceContainer.AddService(typeof(IGenericTestService<>), typeof(GenericTestService<>));
            this._serviceContainer.AddService<ITestService, TestServiceWithGenericSubService>();
            this._serviceContainer.GetService<ITestService>();
        }

        [TestMethod("Try to register a generic interface as singleton service.")]
        public void TryToRegisterGenericInterfaceAsSingleton()
        {
            Assert.ThrowsException<RegisterGenericSingletonServiceException>(() => this._serviceContainer.AddSingleton(typeof(IGenericTestService<>), typeof(GenericTestService<>)));
        }
        #endregion
    }
}
