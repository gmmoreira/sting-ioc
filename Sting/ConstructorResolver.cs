using Sting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sting
{
    public class ConstructorResolver : IConstructorResolver
    {
        private IContainer Container { get; }

        public ConstructorResolver(IContainer container)
        {
            Container = container;
        }

        public ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();

            return constructors.ThrowIfNoPublicConstructor()
                .Where(DependenciesAreResolvable)
                .OrderByDescending(c => c.GetParameters().Count())
                .EmptyConstructAsDefault(type)
                .FirstOrDefault();
        }

        private bool DependenciesAreResolvable(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Count() > 0 &&
                constructor.GetParameters().All(p => Container.IsRegistered(p.ParameterType));
        }
    }

    static class EnumerableExtensions
    {
        public static IEnumerable<ConstructorInfo> ThrowIfNoPublicConstructor(this IEnumerable<ConstructorInfo> enumerable)
        {
            if (!enumerable.Any())
                throw new NoPublicConstructorException();

            return enumerable;
        }

        public static IEnumerable<ConstructorInfo> EmptyConstructAsDefault(this IEnumerable<ConstructorInfo> enumerable, Type type)
        {
            return enumerable.DefaultIfEmpty(type.GetConstructor(new Type[] { }));
        }
    }
}
