using Moq;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Sting.Tests
{
    [TestFixture]
    public class SingletonFactoryTests
    {
        private Mock<IConstructorResolver> constructorResolver;
        private Mock<IDependencyResolver> dependencyResolver;

        [SetUp]
        public void SetUp()
        {
            constructorResolver = new Mock<IConstructorResolver>();
            dependencyResolver = new Mock<IDependencyResolver>();
        }

        [Test]
        public void ItshouldReturnSameInstance()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterlessClass.GetParameterlessConstructor()).Verifiable();
            dependencyResolver.Setup(r => r.Resolve(It.IsAny<ConstructorInfo>())).Returns(new object[] { });
            var factory = new SingletonFactory(typeof(ParameterlessClass), constructorResolver.Object, dependencyResolver.Object);

            var object1 = factory.Build();
            var object2 = factory.Build();

            Assert.That(object1, Is.SameAs(object2));
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