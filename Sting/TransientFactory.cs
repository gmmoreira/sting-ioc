using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sting
{
    public class TransientFactory : IServiceFactory
    {
        public Type ImplementationType { get; }

        public TransientFactory(Type implementationType)
        {
            ImplementationType = implementationType;
        }

        public virtual object Build()
        {
            return ImplementationType.GetConstructors().First().Invoke(new object[] { });
        }
    }
}
