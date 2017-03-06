using Moq;
using Xunit;
using System.Linq;
using System.Reflection;

namespace Sting.Tests
{
    public class DependencyResolverTests
    {
        private Mock<IContainer> container;
        private DependencyResolver resolver;

        public DependencyResolverTests()
        {
            container = new Mock<IContainer>();
            resolver = new DependencyResolver(container.Object);
        }

        [Fact]
        public void GivenAConstructorInfoItShouldResolveAllDependencies()
        {
            var constructorInfo = typeof(ClassTest).GetConstructors().First();
            container.Setup(c => c.Resolve(typeof(ITest1))).Returns(new Test1());
            container.Setup(c => c.Resolve(typeof(ITest2))).Returns(new Test2());
            container.Setup(c => c.Resolve(typeof(ITest3))).Returns(new Test3());

            var result = resolver.Resolve(constructorInfo);

            Assert.Equal(3, result.Length);
            Assert.IsAssignableFrom<ITest1>(result[0]);
            Assert.IsAssignableFrom<ITest2>(result[1]);
            Assert.IsAssignableFrom<ITest3>(result[2]);
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