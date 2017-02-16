using System;
using System.Linq;

namespace Sting
{
    public class TransientFactory : IServiceFactory
    {
        public IConstructorResolver ConstructorResolver { get; }
        public Type ImplementationType { get; }

        public TransientFactory(Type implementationType, IConstructorResolver constructorResolver)
        {
            ConstructorResolver = constructorResolver;
            ImplementationType = implementationType;
        }

        public virtual object Build()
        {
            return ImplementationType.GetConstructors().First().Invoke(new object[] { });
        }
    }
}
