using Moq;
using Xunit;
using Sting.Exceptions;
using System;
using System.Reflection;

namespace Sting.Tests
{
    public class ConstructorResolverTests
    {
        private IConstructorResolver constructorResolver;
        private Mock<IContainer> container;

        public ConstructorResolverTests()
        {
            container = new Mock<IContainer>();
            constructorResolver = new ConstructorResolver(container.Object);
        }

        [Fact]
        public void ItShouldThrowExceptionWhenNoPublicConstructorIsAvailable()
        {
            Assert.Throws<NoPublicConstructorException>(() =>
            {
                return constructorResolver.GetConstructor(typeof(NoPublicConstructor));
            });
        }

        [Fact]
        public void ItShouldReturnDefaultConstructor()
        {
            var constructor = constructorResolver.GetConstructor(typeof(DefaultConstructor));

            Assert.NotNull(constructor);
        }

        [Fact]
        public void ItShouldResolveEmptyConstructorIfDependenciesAreNotResolved()
        {
            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.Equal(constructor,MultipleConstructor.EmptyConstructor());
        }

        [Fact]
        public void ItShouldResolveParameterConstructorIfDependenciesAreResolved()
        {
            container.Setup(c => c.IsRegistered(typeof(ITest))).Returns(true);

            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.Equal(constructor, MultipleConstructor.ParameterConstructor());
        }

        [Fact]
        public void WhenAllDependenciesAreMetItShouldReturnMostParameterConstructor()
        {
            container.Setup(c => c.IsRegistered(typeof(ITest))).Returns(true);
            container.Setup(c => c.IsRegistered(typeof(ITest2))).Returns(true);

            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.Equal(constructor, MultipleConstructor.MostParameterConstructor());
        }

        private interface ITest
        { }

        private interface ITest2
        { }

        private class NoPublicConstructor
        {
            private NoPublicConstructor()
            { }
        }

        private class DefaultConstructor
        { }

        private class MultipleConstructor
        {
            public MultipleConstructor()
            {

            }

            public MultipleConstructor(ITest test)
            {

            }

            public MultipleConstructor(ITest test, ITest2 test2)
            { }


            public static ConstructorInfo EmptyConstructor()
            {
                return typeof(MultipleConstructor).GetConstructor(new Type[] { });
            }

            public static ConstructorInfo ParameterConstructor()
            {
                return typeof(MultipleConstructor).GetConstructor(new Type[] { typeof(ITest) });
            }

            public static ConstructorInfo MostParameterConstructor()
            {
                return typeof(MultipleConstructor).GetConstructor(new Type[] { typeof(ITest), typeof(ITest2) });
            }
        }
    }
}