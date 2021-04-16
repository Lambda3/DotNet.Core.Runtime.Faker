using Moq;
using System;

namespace DotNet.Core.Runtime.Faker.Moq
{
    public static class ServiceProviderExtensions
    {
        public static void ChangeFake<T>(this IServiceProvider services, Action<Mock<T>> configure) where T : class
        {
            var mock = new Mock<T>();
            configure.Invoke(mock);
            services.ChangeFake(mock.Object);
        }
    }
}
