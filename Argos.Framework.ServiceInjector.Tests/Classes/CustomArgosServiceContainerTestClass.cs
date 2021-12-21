using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using System;
using System.Collections.Generic;

namespace Argos.Framework.ServiceInjector.Tests.Classes
{
    public class CustomArgosServiceContainerTestClass : IArgosServiceContainer
    {
        #region Properties
        public IReadOnlyList<IArgosServiceProvider> ServiceProviders { get; }
        #endregion

        #region Methods & Functions
        public void AddService(Type template, Type implementation)
        {
        }

        public void AddService<I, T>() where T : class
        {
        }

        public void AddServiceProvider(IArgosServiceProvider serviceProvider)
        {
        }

        public void AddServiceProviders(params IArgosServiceProvider[] serviceProviders)
        {
        }

        public void AddSingleton(Type template, Type implementation)
        {

        }

        public void AddSingleton<I, T>() where T : class
        {
        }

        public bool ContainsService<T>() => false;

        public bool ContainsService(Type template) => false;

        public object GetGenericService(Type template, params Type[] implementations) => null;

        public object GetService(Type template) => null;

        public I GetService<I>() => default;

        public void RemoveService(Type template)
        {
        }

        public void RemoveService<I>()
        {
        }

        public void RemoveAllServices()
        {
        }

        public void RemoveAllServiceProviders()
        {
        }
        #endregion
    }
}
