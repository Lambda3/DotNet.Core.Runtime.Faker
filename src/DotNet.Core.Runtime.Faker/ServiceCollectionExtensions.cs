using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DotNet.Core.Runtime.Faker
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceWithFaker<T>(this IServiceCollection services, Func<T> configure) where T : class
        {
            services.TryAddSingleton<RuntimeFaker>();

            services.AddTransient(sp =>
            {
                var runtimeFaker = sp.GetRequiredService<RuntimeFaker>();

                var faker = runtimeFaker.Get<T>();
                if (faker == null)
                {
                    faker = configure.Invoke();
                    runtimeFaker.Register(faker);
                }
                return faker;
            });
        }
    }
}
