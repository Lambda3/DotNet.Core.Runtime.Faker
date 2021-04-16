using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNet.Core.Runtime.Faker
{
    public static class ServiceProviderExtensions
    {
        private static RuntimeFaker GetRuntimeFaker<T>(this IServiceProvider services) where T : class
        {
            var faker = services.GetRequiredService<RuntimeFaker>();
            _ = services.GetRequiredService<T>();
            return faker;
        }

        public static void ResetAllFakeChanges(this IServiceProvider services) =>
            services.GetRequiredService<RuntimeFaker>().ResetAllChanges();

        public static void ResetFakeChange<T>(this IServiceProvider services) where T : class =>
            services.GetRequiredService<RuntimeFaker>().ResetChange<T>();

        public static void ChangeFake<T>(this IServiceProvider services, T fake) where T : class =>
            services.GetRuntimeFaker<T>().Change(fake);

        public static T GetFake<T>(this IServiceProvider services) where T : class =>
            services.GetRuntimeFaker<T>().Get<T>();
    }
}
