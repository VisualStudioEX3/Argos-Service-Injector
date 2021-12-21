using System;

namespace Argos.Framework.ServiceInjector.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to register a generic service as singleton service.
    /// </summary>
    public sealed class RegisterGenericSingletonServiceException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that throw the exception.</param>
        public RegisterGenericSingletonServiceException(Type type)
            : base($"You can't register \"{type.Name}\" interface as singleton service because is a generic interface.")
        {
        }
        #endregion
    }
}
