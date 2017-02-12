using NUnit.Framework;

namespace Sting.Tests
{
    [TestFixture()]
    public class SingletonFactoryTests
    {
        [Test()]
        public void ItshouldReturnSameInstance()
        {
            var factory = new SingletonFactory(typeof(Impl));

            var one = factory.Build();
            var two = factory.Build();

            Assert.That(one, Is.SameAs(two));
        }

        private class Impl { }
    }
}