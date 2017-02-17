using System;

namespace Sting
{
    public class SingletonFactory : TransientFactory
    {
        private object Instance { get; set; }

        public SingletonFactory(Type implementationType, IConstructorResolver constructorResolver, IDependencyResolver dependencyResolver) 
            : base(implementationType, constructorResolver, dependencyResolver)
        {
        }

        public override object Build()
        {
            return Instance ?? (Instance = base.Build());
        }
    }
}
