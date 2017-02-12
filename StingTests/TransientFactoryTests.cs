using NUnit.Framework;
using Sting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sting.Tests
{
    [TestFixture()]
    public class TransientFactoryTests
    {
        [Test()]
        public void ItShouldReturnANewInstance()
        {
            var factory = new TransientFactory(typeof(Impl));

            var one = factory.Build();
            var two = factory.Build();

            Assert.That(one, Is.Not.SameAs(two));
        }

        private class Impl { }
    }
}