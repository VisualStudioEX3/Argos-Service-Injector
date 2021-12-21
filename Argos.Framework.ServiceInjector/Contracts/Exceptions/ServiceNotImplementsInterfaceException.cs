using System;

namespace Argos.Framework.ServiceInjector.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to register a service that not implements the required interface.
    /// </summary>
    /// <remarks>You must fully implements the interface in your service implementation.</remarks>
    public sealed class ServiceNotImplementsInterfaceException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="template"><see cref="Type"/> interface that service must implement.</param>
        /// <param name="implementation"><see cref="Type"/> that throw the exception.</param>
        public ServiceNotImplementsInterfaceException(Type template, Type implementation)
            : base(@$"The type ""{implementation.Name}"" must be implements the ""{template.Name}"" interface. 
If the interface is generic or use generic arguments or generic parameters, check if you are implemented in the same way in your service.")
        {
        }
        #endregion
    }
}
