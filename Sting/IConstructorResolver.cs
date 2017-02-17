using System;
using System.Reflection;

namespace Sting
{
    public interface IConstructorResolver
    {
        ConstructorInfo GetConstructor(Type type);
    }
}
