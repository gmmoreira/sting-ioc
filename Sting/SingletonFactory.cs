using System;

namespace Sting
{
    public class SingletonFactory : TransientFactory
    {
        private object Instance { get; set; }

        public SingletonFactory(Type implementationType) : base(implementationType)
        {
        }

        public override object Build()
        {
            return Instance ?? (Instance = base.Build());
        }
    }
}
