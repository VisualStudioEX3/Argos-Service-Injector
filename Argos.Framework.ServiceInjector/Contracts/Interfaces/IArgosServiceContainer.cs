using System;

namespace Argos.Framework.ServiceInjector.Contracts.Interfaces
{
    /// <summary>
    /// A container object to store and request services.
    /// </summary>
    /// <remarks>Use this interface to implements an object to register and store a group of related classes using their interfaces.</remarks>
    public interface IArgosServiceContainer : IArgosServiceProvider
    {
        #region Methods & Functions
        /// <summary>
        /// Adds a <see cref="IArgosServiceProvider"/> reference to this container used to resolve external dependencies.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> reference.</param>
        void AddServiceProvider(IArgosServiceProvider serviceProvider);

        /// <summary>
        /// Adds a list of <see cref="IArgosServiceProvider"/> references to this container used to resolve external dependencies.
        /// </summary>
        /// <param name="serviceProviders">Array of <see cref="IServiceProvider"/> references.</param>
        void AddServiceProviders(params IArgosServiceProvider[] serviceProviders);

        /// <summary>
        /// Removes all <see cref="IArgosServiceContainer"/> references.
        /// </summary>
        void RemoveAllServiceProviders();

        /// <summary>
        /// Adds a service.
        /// </summary>
        /// <param name="template">Interface to register this service.</param>
        /// <param name="implementation">Class used as implementation for this service.</param>
        void AddService(Type template, Type implementation);

        /// <summary>
        /// Adds a service.
        /// </summary>
        /// <typeparam name="I">Interface to register this service.</typeparam>
        /// <typeparam name="T">Class used as implementation for this service.</typeparam>
        void AddService<I, T>() where T : class;

        /// <summary>
        /// Adds a singleton service.
        /// </summary>
        /// <param name="template">Interface to register this singleton service.</param>
        /// <param name="implementation">Class used as implementation for this singleton service.</param>
        void AddSingleton(Type template, Type implementation);

        /// <summary>
        /// Adds a singleton service.
        /// </summary>
        /// <typeparam name="I">Interface to register this singleton service.</typeparam>
        /// <typeparam name="T">Class used as implementation for this singleton service.</typeparam>
        void AddSingleton<I, T>() where T : class;

        /// <summary>
        /// Removes a registered service in this container.
        /// </summary>
        /// <param name="template">Interface used when registered the required service.</param>
        void RemoveService(Type template);

        /// <summary>
        /// Removes a registered service in this container.
        /// </summary>
        /// <typeparam name="I">Interface used when registered the required service.</typeparam>
        void RemoveService<I>();

        /// <summary>
        /// Removes all registered services in this container.
        /// </summary>
        void RemoveAllServices();
        #endregion
    }
}