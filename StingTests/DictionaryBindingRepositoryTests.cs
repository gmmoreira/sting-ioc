using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Sting.Tests
{
    public class DictionaryBindingRepositoryTests
    {
        private DictionaryBindingRepository repository;
        private Mock<IDictionary<Type, IBinding>> dict;
        public DictionaryBindingRepositoryTests()
        {
            dict = new Mock<IDictionary<Type, IBinding>>();
            repository = new DictionaryBindingRepository(dict.Object);
        }

        [Fact]
        public void ItShouldAddBinding()
        {
            var binding = GenericBinding();

            repository.Add(binding);

            dict.Verify(d => d.Add(binding.ServiceType, binding));
        }

        [Fact]
        public void ItShouldVerifyIfExists()
        {
            dict.Setup(d => d.ContainsKey(typeof(ITest))).Returns(true);

            var result = repository.Exists(typeof(ITest));

            Assert.True(result);
        }

        [Fact]
        public void ItShouldReturnBinding()
        {
            var binding = GenericBinding();
            dict.Setup(d => d[typeof(ITest)]).Returns(binding);

            var result = repository.Get(typeof(ITest));

            Assert.Same(binding, result);
        }

        private IBinding GenericBinding()
        {
            var mock = new Mock<IBinding>();
            mock.SetupGet(b => b.ServiceType).Returns(typeof(ITest));
            mock.SetupGet(b => b.ImplementationType).Returns(typeof(Test));

            return mock.Object;
        }

        private interface ITest {}
        private class Test : ITest {}
    }
}