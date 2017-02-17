using System;

namespace Sting
{
    public class TransientFactory : IServiceFactory
    {
        public IConstructorResolver ConstructorResolver { get; }
        public IDependencyResolver DependencyResolver { get; }
        public Type ImplementationType { get; }

        public TransientFactory(Type implementationType, IConstructorResolver constructorResolver, IDependencyResolver dependencyResolver)
        {
            ConstructorResolver = constructorResolver;
            DependencyResolver = dependencyResolver;
            ImplementationType = implementationType;
        }

        public virtual object Build()
        {
            var constructor = ConstructorResolver.GetConstructor(ImplementationType);
            var parameters = DependencyResolver.Resolve(constructor);

            return constructor.Invoke(parameters);
        }
    }
}
