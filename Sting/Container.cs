using System;
using System.Collections.Generic;

namespace Sting
{
    public class Container : IContainer
    {
        private IDictionary<Type, Binding> Storage { get; }

        public Container()
        {
            Storage = new Dictionary<Type, Binding>();
        }

        public void Register(Type service, Type impl)
        {
            Register(service, impl, new TransientFactory(impl, ConstructorResolver(), DependencyResolver()));
        }

        public void Register<TService, TImpl>()
        {
            Register(typeof(TService), typeof(TImpl));
        }

        private void Register(Type service, Type impl, IServiceFactory factory)
        {
            var binding = new Binding(service, impl, factory);
            Storage.Add(service, binding);
        }

        public void RegisterSingleton(Type service, Type impl)
        {
            Register(service, impl, new SingletonFactory(impl, ConstructorResolver(), DependencyResolver()));
        }

        public void RegisterSingleton<TService, TImpl>()
        {
            RegisterSingleton(typeof(TService), typeof(TImpl));
        }

        public TService Resolve<TService>()
        {
            return (TService) Resolve(typeof(TService));
        }

        public object Resolve(Type type)
        {
            var binding = Storage[type];
            return binding.Build();
        }

        public bool IsRegistered<TService>()
        {
            return IsRegistered(typeof(TService));
        }

        public bool IsRegistered(Type type)
        {
            return Storage.ContainsKey(type);
        }

        #region HelperMethods

        private IConstructorResolver ConstructorResolver()
        {
            return new ConstructorResolver(this);
        }

        private IDependencyResolver DependencyResolver()
        {
            return new DependencyResolver(this);
        }

        #endregion
    }
}
