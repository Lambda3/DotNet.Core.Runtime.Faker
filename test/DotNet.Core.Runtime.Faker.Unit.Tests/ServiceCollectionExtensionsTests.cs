using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DotNet.Core.Runtime.Faker.Unit.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Test]
        public void ShouldAddSingletonRuntimeFaker()
        {
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => new MyClass());

            using var serviceProvider = service.BuildServiceProvider();
            serviceProvider.GetService<RuntimeFaker>().Should().NotBeNull();
        }

        [Test]
        public void ShouldGetFakerIfAlreadyExist()
        {
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => new MyClass());

            using var serviceProvider = service.BuildServiceProvider();
            serviceProvider.GetService<MyClass>().Should().NotBeNull();
        }

        public class MyClass
        {
            public int MyProperty { get; set; }
        }
    }
}
