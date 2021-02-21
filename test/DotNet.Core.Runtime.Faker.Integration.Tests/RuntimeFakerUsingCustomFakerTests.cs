using DotNet.Core.Faker.Runtime.WebApi.Sample;
using DotNet.Core.Runtime.Faker.WebApi.Sample;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DotNet.Core.Runtime.Faker;

namespace DotNet.Core.Faker.Runtime.Integration.Tests
{
    public class RuntimeFakerUsingCustomFakerTests
    {
        protected static DateTime changedValue;
        private WebApplicationFactory<Startup> factory;
        private IServiceProvider serviceProvider;
        private HttpClient cliente;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            changedValue = new Bogus.Faker().Date.Past();

            factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                {
                    services.AddServiceWithFaker(() => new Clock());
                }));
            serviceProvider = factory.Services;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => factory.Dispose();

        [SetUp]
        public void Setup() => cliente = factory.CreateClient();

        [TearDown]
        public void TearDown()
        {
            cliente.Dispose();
            serviceProvider.ResetAllChanges();
        }

        [Test]
        public async Task ShouldChangeClockImplementation()
        {
            serviceProvider.Change<Clock>(new MyClock());

            var result = await cliente.GetAsync("time");
            result.StatusCode.Should().Be(200);

            var content = await result.Content.ReadAsStringAsync();

            content.Should().Be($"{changedValue:yyyy-MM-ddThh:mm:ss}");
        }

        public class MyClock : Clock
        {
            public override DateTime Now() => changedValue;
        }
    }
}