using Moq;
using Xunit;

namespace Sting.Tests
{
    public class BindingTests
    {
        [Fact]
        public void ItShouldUseServiceFactory()
        {
            var factory = new Mock<IServiceFactory>();
            var binding = new Binding(typeof(ITest), typeof(Impl), factory.Object);

            binding.Build();

            factory.Verify(f => f.Build());
        }

        private interface ITest { }
        private class Impl { }
    }
}