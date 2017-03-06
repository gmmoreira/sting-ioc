using Moq;
using Xunit;
using System;
using System.Reflection;

namespace Sting.Tests
{
    public class SingletonFactoryTests
    {
        private Mock<IConstructorResolver> constructorResolver;
        private Mock<IDependencyResolver> dependencyResolver;

        public SingletonFactoryTests()
        {
            constructorResolver = new Mock<IConstructorResolver>();
            dependencyResolver = new Mock<IDependencyResolver>();
        }

        [Fact]
        public void ItshouldReturnSameInstance()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterlessClass.GetParameterlessConstructor()).Verifiable();
            dependencyResolver.Setup(r => r.Resolve(It.IsAny<ConstructorInfo>())).Returns(new object[] { });
            var factory = new SingletonFactory(typeof(ParameterlessClass), constructorResolver.Object, dependencyResolver.Object);

            var object1 = factory.Build();
            var object2 = factory.Build();

            Assert.Same(object1, object2);
        }

        private class ParameterlessClass
        {
            public static ConstructorInfo GetParameterlessConstructor()
            {
                return typeof(ParameterlessClass).GetConstructor(new Type[] { });
            }
        }
    }
}