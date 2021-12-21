using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using System;

namespace Argos.Framework.ServiceInjector
{
    /// <summary>
    /// Abstract class that implements a ready to use <see cref="IArgosServiceProvider"/> singleton implementation.
    /// </summary>
    /// <remarks>This class creates an internal <see cref="IArgosServiceContainer"/> instance and register the specified services declared 
    /// in abstract <see cref="RegisterServices(IArgosServiceContainer)"/> function, and exposes a public <see cref="IArgosServiceProvider"/> singleton instance through the 
    /// <see cref="ServiceProvider"/> property, for request the services.
    /// 
    /// <para>Use this abstraction to ease create and setup <see cref="IArgosServiceProvider"/> singleton instances in your project.</para></remarks>
    public abstract class AbstractArgosServiceProvider<T> where T : AbstractArgosServiceProvider<T>
    {
        #region Internal vars
        private static IArgosServiceProvider _serviceProvider;
        #endregion

        #region Properties
        /// <summary>
        /// Singleton instance of this <see cref="IArgosServiceProvider"/> instance.
        /// </summary>
        public static IArgosServiceProvider ServiceProvider
        {
            get
            {
                if (AbstractArgosServiceProvider<T>._serviceProvider is null)
                    Activator.CreateInstance<T>(); // Force to execute the abstract constructor.

                return AbstractArgosServiceProvider<T>._serviceProvider;
            }
            set => AbstractArgosServiceProvider<T>._serviceProvider ??= value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes this <see cref="IArgosServiceProvider"/> instance.
        /// </summary>
        public AbstractArgosServiceProvider()
        {
            IArgosServiceProvider serviceProvider = ArgosServiceProviderFactory.CreateServiceContainer(this.RegisterServices);
            AbstractArgosServiceProvider<T>.ServiceProvider = serviceProvider;
        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Abstract method where register the services for the internal <see cref="IArgosServiceContainer"/>.
        /// </summary>
        /// <param name="serviceContainer">The internal <see cref="IArgosServiceContainer"/> instance.</param>
        public abstract void RegisterServices(IArgosServiceContainer serviceContainer);
        #endregion
    }
}
