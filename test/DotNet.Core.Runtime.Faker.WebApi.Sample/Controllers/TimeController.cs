using DotNet.Core.Runtime.Faker.WebApi.Sample;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Core.Faker.Runtime.WebApi.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly Clock clock;

        public TimeController(Clock clock) => this.clock = clock;

        [HttpGet]
        public string Now() => $"{clock.Now():yyyy-MM-ddThh:mm:ss}";
    }
}
