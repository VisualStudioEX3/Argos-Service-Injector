using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using System;
using Argos.Framework.ServiceInjector.Services;

namespace Argos.Framework.ServiceInjector
{
    /// <summary>
    /// Factory class with functions to create <see cref="IArgosServiceContainer"/> instances.
    /// </summary>
    public static class ArgosServiceProviderFactory
    {
        #region Internal vars
        private static Type _serviceContainerImplementation;
        #endregion

        #region Constructor
        static ArgosServiceProviderFactory() => ArgosServiceProviderFactory.SetCustomServiceContainerImplementation<ArgosServiceContainer>(); 
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Sets a custom <see cref="IArgosServiceContainer"/> implementation to use with this factory.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IArgosServiceContainer"/> interface.</typeparam>
        /// <remarks>This function allow to developers to changes or improves the service container implementation in your projects 
        /// without changing the service container creation call using this factory.</remarks>
        public static void SetCustomServiceContainerImplementation<T>() where T : IArgosServiceContainer => ArgosServiceProviderFactory._serviceContainerImplementation = typeof(T);

        /// <summary>
        /// Creates a new <see cref="IArgosServiceContainer"/> instance.
        /// </summary>
        /// <returns>Returns a new ready <see cref="IArgosServiceContainer"/> instance to store services to request them when are needed.</returns>
        public static IArgosServiceContainer CreateServiceContainer() => (IArgosServiceContainer)Activator.CreateInstance(ArgosServiceProviderFactory._serviceContainerImplementation);

        /// <summary>
        /// Creates a new <see cref="IArgosServiceContainer"/> instance.
        /// </summary>
        /// <param name="registerServices">Lambda method where setup and register the services for this container.</param>
        /// <returns>Returns a new ready <see cref="IArgosServiceContainer"/> instance to store services to request them when are needed.</returns>
        public static IArgosServiceContainer CreateServiceContainer(Action<IArgosServiceContainer> registerServices)
        {
            IArgosServiceContainer container = ArgosServiceProviderFactory.CreateServiceContainer();

            registerServices(container);

            return container;
        }
        #endregion
    }
}
