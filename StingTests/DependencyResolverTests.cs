using NUnit.Framework;
using Moq;
using System.Linq;

namespace Sting.Tests
{
    [TestFixture]
    public class DependencyResolverTests
    {
        private Mock<IContainer> container;
        private DependencyResolver resolver;

        [SetUp]
        public void SetUp()
        {
            container = new Mock<IContainer>();
            resolver = new DependencyResolver(container.Object);
        }

        [Test]
        public void GivenAConstructorInfoItShouldResolveAllDependencies()
        {
            var constructorInfo = typeof(ClassTest).GetConstructors().First();
            container.Setup(c => c.Resolve(typeof(ITest1))).Returns(new Test1());
            container.Setup(c => c.Resolve(typeof(ITest2))).Returns(new Test2());
            container.Setup(c => c.Resolve(typeof(ITest3))).Returns(new Test3());

            var result = resolver.Resolve(constructorInfo);

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], Is.InstanceOf<ITest1>());
            Assert.That(result[1], Is.InstanceOf<ITest2>());
            Assert.That(result[2], Is.InstanceOf<ITest3>());
        }

        private interface ITest1 { } private class Test1 : ITest1 { }
        private interface ITest2 { } private class Test2 : ITest2 { }
        private interface ITest3 { } private class Test3 : ITest3 { }

        private class ClassTest
        {
            public ClassTest(ITest1 t1, ITest2 t2, ITest3 t3)
            { }
        }
    }
}