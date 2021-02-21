using System;

namespace DotNet.Core.Runtime.Faker.WebApi.Sample
{
    public class Clock
    {
        public virtual DateTime Now() => DateTime.Now;
    }
}
