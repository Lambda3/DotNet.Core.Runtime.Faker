using FakeItEasy;
using System;

namespace DotNet.Core.Runtime.Faker.FakeItEasy
{
    public static class ServiceProviderExtensions
    {
        public static void ChangeFake<T>(this IServiceProvider services, Action<T> configure) where T : class
        {
            var fake = A.Fake<T>();
            configure.Invoke(fake);
            services.ChangeFake(fake);
        }
    }
}
