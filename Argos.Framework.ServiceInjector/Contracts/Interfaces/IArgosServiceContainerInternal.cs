using Argos.Framework.ServiceInjector.Contracts.Models;
using System;
using System.Collections.Generic;

namespace Argos.Framework.ServiceInjector.Contracts.Interfaces
{
    internal interface IArgosServiceContainerInternal : IArgosServiceContainer
    {
        #region Methods & Functions
        IReadOnlyDictionary<Type, ArgosServiceModel> GetInternalServiceDictionary();
        #endregion
    }
}
