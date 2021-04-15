using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DotNet.Core.Runtime.Faker.Unit.Tests
{
    public class ServiceProviderExtensionsTests
    {
        [Test]
        public void ShouldResetAllChanges()
        {
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => new MyClass());

            using var serviceProvider = service.BuildServiceProvider();
            serviceProvider.Change<MyClass>(new MyClass2());

            serviceProvider.ResetAllChanges();
            serviceProvider.GetService<MyClass>().Should().BeOfType<MyClass>();
        }

        [Test]
        public void ShouldResetChanges()
        {
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => new MyClass());

            using var serviceProvider = service.BuildServiceProvider();
            serviceProvider.Change<MyClass>(new MyClass2());

            serviceProvider.ResetChange<MyClass>();
            serviceProvider.GetService<MyClass>().Should().BeOfType<MyClass>();
        }

        [Test]
        public void ShouldChange()
        {
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => new MyClass());

            using var serviceProvider = service.BuildServiceProvider();
            serviceProvider.Change<MyClass>(new MyClass2());

            serviceProvider.GetService<MyClass>().Should().BeOfType<MyClass2>();
        }

        [Test]
        public void ShouldGet()
        {
            var registeredFake = new MyClass();
            var service = new ServiceCollection();
            service.AddServiceWithFaker(() => registeredFake);

            using var serviceProvider = service.BuildServiceProvider();
            var fake = serviceProvider.Get<MyClass>();

            fake.Should().Be(registeredFake);
        }

        public class MyClass
        {
            public int MyProperty { get; set; }
        }

        public class MyClass2 : MyClass
        {
        }
    }
}
