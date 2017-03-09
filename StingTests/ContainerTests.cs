using Xunit;
using Moq;

namespace Sting.Tests
{
    public class ContainerTests
    {
        private IContainer container;
        private Mock<IBindingRepository> repository;

        public ContainerTests()
        {
            repository = new Mock<IBindingRepository>();
            container = new Container();
        }

        [Fact]
        public void ItShouldBeAbleToRegisterAndResolveService()
        {
            container.Register<ITest, Impl>();

            var resolvedService = container.Resolve<ITest>();

            Assert.IsAssignableFrom<ITest>(resolvedService);
            Assert.IsType<Impl>(resolvedService);                      
        }
        
        [Fact]
        public void RegisteringSingletonShouldResolveSameObject()
        {
            container.RegisterSingleton<ITest, Impl>();

            var one = container.Resolve<ITest>();
            var two = container.Resolve<ITest>();

            Assert.Same(one, two);
        }

        [Fact]
        public void ShouldBeAbleToVerifyIfTypeIsRegistered()
        {
            container.Register<ITest, Impl>();

            var result = container.IsRegistered<ITest>();

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseWhenTypeIsNotRegistered()
        {
            container.Register<ITest, Impl>();

            var result = container.IsRegistered<INonExists>();

            Assert.False(result);
        }

        private interface ITest
        {

        }

        private interface INonExists
        {

        }

        private class Impl : ITest
        {

        }
    }
}