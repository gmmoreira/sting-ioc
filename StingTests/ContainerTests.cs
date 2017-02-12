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
    public class ContainerTests
    {
        private IContainer container;

        [SetUp]
        public void SetUp()
        {
            container = new Container();
        }

        [Test()]
        public void ItShouldBeAbleToRegisterAndResolveService()
        {
            container.Register<ITest, Impl>();

            var resolvedService = container.Resolve<ITest>();

            Assert.That(resolvedService, Is.InstanceOf<ITest>());
        }

        private interface ITest
        {

        }

        private class Impl : ITest
        {

        }
    }
}