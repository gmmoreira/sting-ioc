﻿using NUnit.Framework;
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
            Assert.That(resolvedService, Is.TypeOf<Impl>());
        }
        
        [Test]
        public void RegisteringSingletonShouldResolveSameObject()
        {
            container.RegisterSingleton<ITest, Impl>();

            var one = container.Resolve<ITest>();
            var two = container.Resolve<ITest>();

            Assert.That(one, Is.SameAs(two));
        }

        [Test]
        public void ShouldBeAbleToVerifyIfTypeIsRegistered()
        {
            container.Register<ITest, Impl>();

            var result = container.IsRegistered<ITest>();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldReturnFalseWhenTypeIsNotRegistered()
        {
            container.Register<ITest, Impl>();

            var result = container.IsRegistered<INonExists>();

            Assert.That(result, Is.False);
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