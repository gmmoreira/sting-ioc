using Moq;
using Xunit;
using System;
using System.Reflection;

namespace Sting.Tests
{
    public class TransientFactoryTests
    {
        private Mock<IConstructorResolver> constructorResolver;
        private Mock<IDependencyResolver> dependencyResolver;

        public TransientFactoryTests()
        {
            constructorResolver = new Mock<IConstructorResolver>();
            dependencyResolver = new Mock<IDependencyResolver>();
        }

        [Fact]
        public void ItShouldReturnANewInstance()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterClass.GetParameterlessConstructor());
            dependencyResolver.Setup(r => r.Resolve(ParameterClass.GetParameterlessConstructor())).Returns(new object[] { });
            var factory = new TransientFactory(typeof(ParamaterLessClass), constructorResolver.Object, dependencyResolver.Object);

            var object1 = factory.Build();
            var object2 = factory.Build();

            Assert.NotSame(object1, object2);
        }

        [Fact]
        public void ItShouldRequestAConstructor()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterClass.GetParameterlessConstructor()).Verifiable();
            dependencyResolver.Setup(r => r.Resolve(ParameterClass.GetParameterlessConstructor())).Returns(new object[] { });
            var factory = new TransientFactory(typeof(ParameterClass), constructorResolver.Object, dependencyResolver.Object);

            factory.Build();

            constructorResolver.Verify(r => r.GetConstructor(typeof(ParameterClass)));
        }

        [Fact]
        public void ItShouldRequestParametersForConstructor()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterClass.GetParameterlessConstructor());
            dependencyResolver.Setup(r => r.Resolve(It.IsAny<ConstructorInfo>())).Returns(new object[] { }).Verifiable();
            var factory = new TransientFactory(typeof(ParameterClass), constructorResolver.Object, dependencyResolver.Object);

            factory.Build();

            dependencyResolver.Verify(r => r.Resolve(It.IsAny<ConstructorInfo>()));
        }

        private class ParamaterLessClass { }

        private class ParameterClass
        {
            public ParameterClass()
            {

            }

            public static ConstructorInfo GetParameterlessConstructor()
            {
                return typeof(ParameterClass).GetConstructor(new Type[] { });
            }
        }
    }
}