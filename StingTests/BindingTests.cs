using Moq;
using NUnit.Framework;

namespace Sting.Tests
{
    [TestFixture]
    public class BindingTests
    {
        [Test]
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