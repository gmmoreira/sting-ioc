using Moq;
using NUnit.Framework;
using Sting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sting.Tests
{
    [TestFixture]
    public class TransientFactoryTests
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
        public void ItShouldReturnANewInstance()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterClass.GetParameterlessConstructor());
            dependencyResolver.Setup(r => r.Resolve(ParameterClass.GetParameterlessConstructor())).Returns(new object[] { });
            var factory = new TransientFactory(typeof(ParamaterLessClass), constructorResolver.Object, dependencyResolver.Object);

            var object1 = factory.Build();
            var object2 = factory.Build();

            Assert.That(object1, Is.Not.SameAs(object2));
        }

        [Test]
        public void ItShouldRequestAConstructor()
        {
            constructorResolver.Setup(r => r.GetConstructor(It.IsAny<Type>())).Returns(ParameterClass.GetParameterlessConstructor()).Verifiable();
            dependencyResolver.Setup(r => r.Resolve(ParameterClass.GetParameterlessConstructor())).Returns(new object[] { });
            var factory = new TransientFactory(typeof(ParameterClass), constructorResolver.Object, dependencyResolver.Object);

            factory.Build();

            constructorResolver.Verify(r => r.GetConstructor(typeof(ParameterClass)));
        }

        [Test]
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