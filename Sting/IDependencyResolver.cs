using System.Reflection;

namespace Sting
{
    public interface IDependencyResolver
    {
        object[] Resolve(ConstructorInfo constructorInfo);
    }
}
