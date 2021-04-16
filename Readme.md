# DotNet.Core.Runtime.Faker

Lib para trocar implementações injetadas via DI em tempo de execução nos testes integrados

## Instalação

### Com FakeItEasy

[![NuGet](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy.svg?style=flat)](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy.svg?style=flat)

[NuGet package](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy/) disponível:
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy
```

### Com Moq

[![NuGet](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.Moq.svg?style=flat)](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.Moq.svg?style=flat)

[NuGet package](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.Moq/) disponível:
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.Moq
```

### Manualmente

[![NuGet](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.svg?style=flat)](https://img.shields.io/nuget/v/Lambda3.DotNet.Core.Runtime.Faker.svg?style=flat)

[NuGet package](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker/) disponível:
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker
```

## Configuração

### Através do [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy)
- Lib Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy 
-  Registrar o faker 
```c#
var registeredValue = new DateTime();

var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
    builder.ConfigureTestServices(services =>
    {
        services.AddServiceWithFaker<Clock>(faker => A.CallTo(() => faker.Now()).Returns(registeredValue));
    }));
var serviceProvider = factory.Services;
```

- Mudar implementação
```c#
serviceProvider.ChangeFake<Clock>(faker => A.CallTo(() => faker.Now()).Returns(new DateTime()));
```

- Receber novo valor

```c#
serviceProvider.GetService<Clock>().Now();
```
Deve retornar valor informado no change =)

- Limpar implementação para não influenciar em outros testes
```c#
serviceProvider.ResetAllFakeChanges();
```

### Através do [Moq](https://github.com/Moq/moq4)
- Lib Lambda3.DotNet.Core.Runtime.Faker.Moq

Muito parecido com o FakeItEasy, mas com a sintaxe do moq
```c#
factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(mock => mock.Setup(x => x.Now()).Returns(registeredValue));
}));
```
e
```c#
 serviceProvider.ChangeFake<Clock>(mock => mock.Setup(x => x.Now()).Returns(new DateTime()));
```

### Manualmente
- Lib Lambda3.DotNet.Core.Runtime.Faker

Muito parecido com os anteriores, mas sem dependências das libs
```c#
factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(() => new FakeClock());
}));
```
e
```c#
serviceProvider.ChangeFake<Clock>(() => new FakeClock());
```

Exemplos completos:
- [Com FakeItEasy](https://github.com/willsbctm/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingFakeItEasyTests.cs)
- [Com Moq](https://github.com/willsbctm/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingMoqTests.cs)
- [Manualmente](https://github.com/willsbctm/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingCustomFakerTests.cs)
