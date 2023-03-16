
[![Build status (DotNet.Core.Runtime.Faker)](https://img.shields.io/github/actions/workflow/status/Lambda3/DotNet.Core.Runtime.Faker/build.yaml)](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/actions/workflows/build.yaml)

| Package | Version   | Downloads | 
| ------------- | ------------- | -- |
| Moq | [![NuGet version (DotNet.Core.Runtime.Faker.Moq)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.Moq/) | [![NuGet version (DotNet.Core.Runtime.Faker.Moq)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker.Moq)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.Moq/) |
| FakeItEasy | [![NuGet version (DotNet.Core.Runtime.Faker.FakeItEasy)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy/) | [![NuGet version (DotNet.Core.Runtime.Faker.FakeItEasy)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy/)
| Manually | [![NuGet version (DotNet.Core.Runtime.Faker)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker/) | [![NuGet version (DotNet.Core.Runtime.Faker)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker/)


# DotNet.Core.Runtime.Faker

Lib to change runtime implementation by DI in integration tests

## Installation

### Using Moq
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.Moq
```

### Using FakeItEasy
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy
```

### Manually
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker
```

## Configuration

### Using [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy)
- Lib DotNet.Core.Runtime.Faker.FakeItEasy 
-  Faker registering 
```c#
var registeredValue = new DateTime();

var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
    builder.ConfigureTestServices(services =>
    {
        services.AddServiceWithFaker<Clock>(faker => A.CallTo(() => faker.Now()).Returns(registeredValue));
    }));
var serviceProvider = factory.Services;
```

- Change implementation
```c#
serviceProvider.Change<Clock>(faker => A.CallTo(() => faker.Now()).Returns(new DateTime()));
```

- Set new value

```c#
serviceProvider.GetService<Clock>().Now();
```
Should returns new value =)

- Clean implementation to avoid problems in anothers tests
```c#
serviceProvider.ResetAllChanges();
```

### Using [Moq](https://github.com/Moq/moq4)
- Lib DotNet.Core.Runtime.Faker.Moq

Very similar with FakeItEasy, but using moq syntax
```c#
factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(mock => mock.Setup(x => x.Now()).Returns(registeredValue));
}));
```
and
```c#
 serviceProvider.Change<Clock>(mock => mock.Setup(x => x.Now()).Returns(new DateTime()));
```

### Manually
- Lib DotNet.Core.Runtime.Faker

Without any libs
```c#
factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(() => new FakeClock());
}));
```
and
```c#
serviceProvider.Change<Clock>(() => new FakeClock());
```

Completed samples:
- [Com FakeItEasy](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingFakeItEasyTests.cs)
- [Com Moq](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingMoqTests.cs)
- [Manualmente](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingCustomFakerTests.cs)