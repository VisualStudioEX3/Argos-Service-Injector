using System;
using System.Collections.Generic;

namespace Argos.Framework.ServiceInjector.Contracts.Interfaces
{
    /// <summary>
    /// A service provider object.
    /// </summary>
    /// <remarks>Use this interface to implements an object to request already registered services in a <see cref="IArgosServiceContainer"/>.</remarks>
    public interface IArgosServiceProvider
    {
        #region Properties
        /// <summary>
        /// Gets a list of the <see cref="IServiceProvider"/>s that this provider used to resolve external dependencies.
        /// </summary>
        IReadOnlyList<IArgosServiceProvider> ServiceProviders { get; }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Gets if this container has registered the requested service.
        /// </summary>
        /// <typeparam name="T">The requested service interface.</typeparam>
        /// <returns>Returns <see langword="true"/> if the service is registered in this container.</returns>
        bool ContainsService<T>();

        /// <summary>
        /// Gets if this container has registered the requested service.
        /// </summary>
        /// <param name="template">The requested service interface.</param>
        /// <returns>Returns <see langword="true"/> if the service is registered in this container.</returns>
        bool ContainsService(Type template);

        /// <summary>
        /// Gets an instance for the required generic service.
        /// </summary>
        /// <param name="template">Interface used when registered the required generic service.</param>
        /// <param name="implementations">Array with all classes that the generic service implements. They can be generic classes or other services registered in this container.</param>
        /// <returns>Returns a new instance of the generic service or the existing one if is a singleton service.</returns>
        object GetGenericService(Type template, params Type[] implementations);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <param name="template">Interface used when registered the required service.</param>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        object GetService(Type template);

        /// <summary>
        /// Gets an instance for the required service.
        /// </summary>
        /// <typeparam name="I">Interface used when registered the required service.</typeparam>
        /// <returns>Returns a new instance of the service or the existing one if is a singleton service.</returns>
        I GetService<I>();
        #endregion
    }
}
