using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sting
{
    public class Container : IContainer
    {
        private IDictionary<Type, Type> Storage { get; }

        public Container()
        {
            Storage = new Dictionary<Type, Type>();
        }

        public void Register(Type service, Type impl)
        {
            Storage.Add(service, impl);
        }

        public void Register<TService, TImpl>()
        {
            Register(typeof(TService), typeof(TImpl));
        }

        public TService Resolve<TService>()
        {
            var serviceType = Storage[typeof(TService)];
            return (TService) serviceType.GetConstructors().First().Invoke(new object[] { });
        }
    }
}
