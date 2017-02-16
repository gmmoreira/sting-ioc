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
            Register(service, impl, new TransientFactory(impl, new ConstructorResolver(this)));
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
            Register(service, impl, new SingletonFactory(impl, new ConstructorResolver(this)));
        }

        public void RegisterSingleton<TService, TImpl>()
        {
            RegisterSingleton(typeof(TService), typeof(TImpl));
        }

        public TService Resolve<TService>()
        {
            var binding = Storage[typeof(TService)];
            return (TService)binding.Build();
        }

        public bool IsRegistered<TService>()
        {
            return IsRegistered(typeof(TService));
        }

        public bool IsRegistered(Type type)
        {
            return Storage.ContainsKey(type);
        }
    }
}
