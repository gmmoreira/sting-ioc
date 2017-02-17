using Moq;
using NUnit.Framework;
using Sting;
using Sting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sting.Tests
{
    [TestFixture]
    public class ConstructorResolverTests
    {
        private IConstructorResolver constructorResolver;
        private Mock<IContainer> container;

        [SetUp]
        public void SetUp()
        {
            container = new Mock<IContainer>();
            constructorResolver = new ConstructorResolver(container.Object);
        }

        [Test]
        public void ItShouldThrowExceptionWhenNoPublicConstructorIsAvailable()
        {
            Assert.That(() =>
            {
                return constructorResolver.GetConstructor(typeof(NoPublicConstructor));
            }, Throws.TypeOf<NoPublicConstructorException>());
        }

        [Test]
        public void ItShouldReturnDefaultConstructor()
        {
            var constructor = constructorResolver.GetConstructor(typeof(DefaultConstructor));

            Assert.That(constructor, Is.Not.Null);
        }

        [Test]
        public void ItShouldResolveEmptyConstructorIfDependenciesAreNotResolved()
        {
            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.That(constructor, Is.EqualTo(MultipleConstructor.EmptyConstructor()));
        }

        [Test]
        public void ItShouldResolveParameterConstructorIfDependenciesAreResolved()
        {
            container.Setup(c => c.IsRegistered(typeof(ITest))).Returns(true);

            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.That(constructor, Is.EqualTo(MultipleConstructor.ParameterConstructor()));
        }

        [Test]
        public void WhenAllDependenciesAreMetItShouldReturnMostParameterConstructor()
        {
            container.Setup(c => c.IsRegistered(typeof(ITest))).Returns(true);
            container.Setup(c => c.IsRegistered(typeof(ITest2))).Returns(true);

            var constructor = constructorResolver.GetConstructor(typeof(MultipleConstructor));

            Assert.That(constructor, Is.EqualTo(MultipleConstructor.MostParameterConstructor()));
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