using System.Linq;
using System.Reflection;

namespace Sting
{
    public class DependencyResolver : IDependencyResolver
    {
        private IContainer Container { get; }

        public DependencyResolver(IContainer container)
        {
            Container = container;
        }

        public object[] Resolve(ConstructorInfo constructorInfo)
        {
            return constructorInfo.GetParameters()
                .Select(p => Container.Resolve(p.ParameterType))
                .ToArray();
        }
    }
}
