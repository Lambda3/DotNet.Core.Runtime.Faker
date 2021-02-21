using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace DotNet.Core.Runtime.Faker.Moq
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceWithFaker<T>(this IServiceCollection services, Action<Mock<T>> configure) where T : class
        {
            var mock = new Mock<T>();
            configure.Invoke(mock);
            services.AddServiceWithFaker(() => mock.Object);
        }

        public static void AddServiceWithFaker<T>(this IServiceCollection services) where T : class => services.AddServiceWithFaker<T>(faker => { });
    }
}
