using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Argos.Framework.ServiceInjector.Contracts.Exceptions;
using Argos.Framework.ServiceInjector.Contracts.Interfaces;
using Argos.Framework.ServiceInjector.Contracts.Models;

namespace Argos.Framework.ServiceInjector.Services
{
    class ArgosServiceContainer : IArgosServiceContainerInternal
    {
        #region Internal vars
        private readonly List<IArgosServiceProvider> _serviceProviders = new();
        private readonly Dictionary<Type, ArgosServiceModel> _services = new();

        private IEnumerable<IArgosServiceContainerInternal> _cachedServiceContainers;
        #endregion

        #region Properties
        public IReadOnlyList<IArgosServiceProvider> ServiceProviders => this._serviceProviders;
        #endregion

        #region Constructors
        public ArgosServiceContainer() => this.UpdateCachedServiceContainerList();
        #endregion

        #region Methods & Functions
        public IReadOnlyDictionary<Type, ArgosServiceModel> GetInternalServiceDictionary() => this._services;

        public void AddServiceProvider(IArgosServiceProvider serviceProvider)
        {
            this._serviceProviders.Add(serviceProvider);
            this.UpdateCachedServiceContainerList();
        }

        public void AddServiceProviders(params IArgosServiceProvider[] serviceProviders)
        {
            this._serviceProviders.AddRange(serviceProviders);
            this.UpdateCachedServiceContainerList();
        }

        public void RemoveAllServiceProviders()
        {
            this._serviceProviders.Clear();
            this.UpdateCachedServiceContainerList();
        }

        private void RegisterService(Type template, Type implementation, bool isSingleton)
        {
            if (!template.IsInterface)
                throw new InvalidServiceInterfaceException(template);

            if (!implementation.IsClass)
                throw new InvalidServiceClassException(implementation);

            if (implementation.IsAbstract)
                throw new ServiceAbstractClassException(implementation);

            if (implementation.IsGenericType && isSingleton)
                throw new RegisterGenericSingletonServiceException(template);

            if (!IsServiceImplementsInterface(template, implementation))
                throw new ServiceNotImplementsInterfaceException(template, implementation);

            if (!_services.TryAdd(template, new ArgosServiceModel(implementation, isSingleton)))
                throw new ServiceAlreadyRegisteredException(implementation);
        }

        public void AddService(Type template, Type implementation) => this.RegisterService(template, implementation, isSingleton: false);

        public void AddService<I, T>() where T : class => this.AddService(typeof(I), typeof(T));

        public void AddSingleton(Type template, Type implementation) => this.RegisterService(template, implementation, isSingleton: true);

        public void AddSingleton<I, T>() where T : class => this.AddSingleton(typeof(I), typeof(T));

        public void RemoveAllServices()
        {
            foreach (Type template in this._services.Keys)
                this.RemoveService(template);
        }

        public void RemoveService<I>() => this.RemoveService(typeof(I));

        public void RemoveService(Type template)
        {
            if (!template.IsInterface)
                throw new InvalidServiceInterfaceException(template);

            if (this._services.TryGetValue(template, out ArgosServiceModel service))
            {
                if (service.isSingleton && template is IDisposable)
                    (service.singletonInstance as IDisposable).Dispose();

                this._services.Remove(template);
            }
            else
                throw new ServiceNotFoundException(template);
        }

        public I GetService<I>() => (I)this.GetService(typeof(I));

        public object GetService(Type template)
        {
            return template.IsInterface
                ? this.ResolveInstance(template, (service) => this.ResolveServiceInstance(service))
                : throw new InvalidServiceInterfaceException(template);
        }

        public object GetGenericService(Type template, params Type[] implementations)
        {
            if (!template.IsInterface)
                throw new InvalidServiceInterfaceException(template);

            if (!template.IsGenericType)
                throw new InterfaceIsNotGenericException(template);

            return this.ResolveInstance(template, (service) => ArgosServiceContainer.ResolveGenericServiceInstance(service, implementations));
        }

        public bool ContainsService<I>() => this.ContainsService(typeof(I));

        public bool ContainsService(Type template) => this._services.ContainsKey(template);

        private object ResolveServiceInstance(ArgosServiceModel service)
        {
            Type type = service.type;
            List<object> arguments = null;

            if (ArgosServiceContainer.TryGetFirstParametrizedConstructorParameters(type, out IEnumerable<ParameterInfo> parameters))
                foreach (ParameterInfo parameter in parameters)
                    this.ResolveConstructorServiceParameter(ref arguments, parameter);

            return arguments is null
                ? Activator.CreateInstance(type)
                : Activator.CreateInstance(type, arguments.ToArray());
        }

        private static object ResolveGenericServiceInstance(ArgosServiceModel service, Type[] implementations)
        {
            Type genericType = service.type.MakeGenericType(implementations);
            return Activator.CreateInstance(genericType);
        }

        private static bool TryGetFirstParametrizedConstructorParameters(Type type, out IEnumerable<ParameterInfo> parameters)
        {
            parameters = null;

            IEnumerable<ConstructorInfo> parametrizedConstructors = type.GetConstructors().Where(e => e.GetParameters().Any());

            if (parametrizedConstructors.Any())
                parameters = parametrizedConstructors.First().GetParameters();

            return parameters is not null;
        }

        private object ResolveInstance(Type template, Func<ArgosServiceModel, object> builder)
        {
            foreach (IArgosServiceContainerInternal serviceContainer in this._cachedServiceContainers)
                if (ArgosServiceContainer.ResolveInstanceFromContainer(serviceContainer, template, builder, out object buildedService))
                    return buildedService;

            throw new ServiceNotFoundException(template);
        }

        private void UpdateCachedServiceContainerList()
        {
            List<IArgosServiceContainerInternal> list = new();

            list.Add(this);
            list.AddRange(this._serviceProviders.Cast<IArgosServiceContainerInternal>());

            this._cachedServiceContainers = list;
        }

        private void ResolveConstructorServiceParameter(ref List<object> arguments, ParameterInfo parameter)
        {
            object argumentService = this.ResolveServiceParameter(parameter);
            arguments ??= new();
            arguments.Add(argumentService);
        }

        private object ResolveServiceParameter(ParameterInfo parameter)
        {
            if (parameter.ParameterType.IsGenericType)
            {
                Type genericTemplate = ArgosServiceContainer.GetGenericTypeDefinitionFromParameterInfo(parameter);
                Type[] genericTypeParameters = ArgosServiceContainer.GetGenericArgumentsFromParameterInfo(parameter);

                return this.GetGenericService(genericTemplate, genericTypeParameters);
            }
            else
                return this.GetService(parameter.ParameterType);
        }

        private static bool IsServiceImplementsInterface(Type template, Type implementation)
        {
            return template.IsGenericType
                ? ArgosServiceContainer.IsGenericServiceImplementsInterface(template, implementation)
                : ArgosServiceContainer.IsNonGenericServiceImplementsInterface(template, implementation);
        }

        private static bool IsNonGenericServiceImplementsInterface(Type template, Type implementation) => implementation.IsAssignableTo(template);

        private static bool IsGenericServiceImplementsInterface(Type template, Type implementation)
        {
            return ArgosServiceContainer.IsImplementationClassImplementsInterface(template, implementation) &&
                   ArgosServiceContainer.ServiceContainsSameGenericParameters(template, implementation);
        }

        private static bool IsImplementationClassImplementsInterface(Type template, Type implementation) => implementation.GetInterface(template.Name) is not null;

        private static bool ServiceContainsSameGenericParameters(Type template, Type implementation)
        {
            return ArgosServiceContainer.ServiceHasGenericParameters(template, implementation) &&
                   ArgosServiceContainer.CheckServiceGenericTypeParameters(template, implementation);
        }

        private static bool ServiceHasGenericParameters(Type template, Type implementation) => template.ContainsGenericParameters && implementation.ContainsGenericParameters;

        private static bool CheckServiceGenericTypeParameters(Type template, Type implementation)
        {
            Type[] templateGenericParameters = ArgosServiceContainer.GetGenericTypeParamatersFromType(template);
            Type[] implementationGenericParameters = ArgosServiceContainer.GetGenericTypeParamatersFromType(implementation);

            return templateGenericParameters.Length == implementationGenericParameters.Length;
        }

        private static Type[] GetGenericTypeParamatersFromType(Type type) => type.GetTypeInfo().GenericTypeParameters;

        private static Type GetGenericTypeDefinitionFromParameterInfo(ParameterInfo parameter) => parameter.ParameterType.GetGenericTypeDefinition();

        private static Type[] GetGenericArgumentsFromParameterInfo(ParameterInfo parameter) => parameter.ParameterType.GetGenericArguments();

        private static bool ResolveInstanceFromContainer(IArgosServiceContainerInternal serviceContainer,
            Type template,
            Func<ArgosServiceModel, object> builder,
            out object buildedService)
        {
            IReadOnlyDictionary<Type, ArgosServiceModel> services = serviceContainer.GetInternalServiceDictionary();

            buildedService = services.TryGetValue(template, out ArgosServiceModel service)
                ? ArgosServiceContainer.BuildService(service, builder)
                : null;

            return buildedService is not null;
        }

        private static object BuildService(ArgosServiceModel service, Func<ArgosServiceModel, object> builder)
        {
            if (service.isSingleton)
            {
                service.singletonInstance ??= builder(service);
                return service.singletonInstance;
            }
            else
                return builder(service);
        }
        #endregion
    }
}
