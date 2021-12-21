using System;

namespace Argos.Framework.ServiceInjector.Contracts.Models
{
    class ArgosServiceModel
    {
        #region Public vars
        public readonly Type type;
        public readonly bool isSingleton;
        public object singletonInstance;
        #endregion

        #region Constructor
        public ArgosServiceModel(Type service, bool isSingleton)
        {
            this.type = service;
            this.isSingleton = isSingleton;
        }
        #endregion
    }
}
