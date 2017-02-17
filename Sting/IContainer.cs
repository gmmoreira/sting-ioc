using System;

namespace Sting
{
    public interface IContainer
    {
        void Register<TService, TImpl>();
        void Register(Type service, Type impl);
        TService Resolve<TService>();
        object Resolve(Type type);
        void RegisterSingleton<TService, TImpl>();
        void RegisterSingleton(Type service, Type impl);
        bool IsRegistered<TService>();
        bool IsRegistered(Type type);
    }
}
