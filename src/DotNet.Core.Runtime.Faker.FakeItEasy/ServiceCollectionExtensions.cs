using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNet.Core.Runtime.Faker.FakeItEasy
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceWithFaker<T>(this IServiceCollection services, Action<T> configure) where T : class =>
            services.AddServiceWithFaker(() => A.Fake<T>(opt => opt.ConfigureFake(configure)));

        public static void AddServiceWithFaker<T>(this IServiceCollection services) where T : class => services.AddServiceWithFaker<T>(faker => { });
    }
}
