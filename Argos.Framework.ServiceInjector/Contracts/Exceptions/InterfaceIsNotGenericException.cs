using System;

namespace Argos.Framework.ServiceInjector.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to compose a generic service from a non generic interface.
    /// </summary>
    public sealed class InterfaceIsNotGenericException : Exception
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that throw the exception.</param>
        public InterfaceIsNotGenericException(Type type)
            : base($"The type \"{type.Name}\" must be a generic interface.")
        {
        }
        #endregion
    }
}
