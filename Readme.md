
[![Build status (DotNet.Core.Runtime.Faker)](https://img.shields.io/github/actions/workflow/status/Lambda3/DotNet.Core.Runtime.Faker/build.yaml)](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/actions/workflows/build.yaml)

| Pacote | Versão   | Downloads | 
| ------------- | ------------- | -- |
| Moq | [![NuGet version (DotNet.Core.Runtime.Faker.Moq)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.Moq/) | [![NuGet version (DotNet.Core.Runtime.Faker.Moq)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker.Moq)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.Moq/) |
| FakeItEasy | [![NuGet version (DotNet.Core.Runtime.Faker.FakeItEasy)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy/) | [![NuGet version (DotNet.Core.Runtime.Faker.FakeItEasy)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy/)
| Manual | [![NuGet version (DotNet.Core.Runtime.Faker)](https://img.shields.io/github/v/release/Lambda3/DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker/) | [![NuGet version (DotNet.Core.Runtime.Faker)](https://img.shields.io/nuget/dt/Lambda3.DotNet.Core.Runtime.Faker)](https://www.nuget.org/packages/Lambda3.DotNet.Core.Runtime.Faker/)

# DotNet.Core.Runtime.Faker

## Read this in other language: [English](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/Readme.en.md)


Lib para trocar implementações injetadas via DI em tempo de execução nos testes integrados

## Instalação

### Com Moq
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.Moq
```

### Com FakeItEasy
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker.FakeItEasy
```

### Manual
```
> dotnet add package Lambda3.DotNet.Core.Runtime.Faker
```

## Configuração

### Através do [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy)
- Lib DotNet.Core.Runtime.Faker.FakeItEasy 
-  Registrar o faker 
```c#
var registeredValue = new DateTime();

var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
    builder.ConfigureTestServices(services =>
    {
        services.AddServiceWithFaker<Clock>(faker => A.CallTo(() => faker.Now()).Returns(registeredValue));
    }));
var serviceProvider = factory.Services;
```

- Mudar implementação
```c#
serviceProvider.Change<Clock>(faker => A.CallTo(() => faker.Now()).Returns(new DateTime()));
```

- Receber novo valor

```c#
serviceProvider.GetService<Clock>().Now();
```
Deve retornar valor informado no change =)

- Limpar implementação para não influenciar em outros testes
```c#
serviceProvider.ResetAllChanges();
```

### Através do [Moq](https://github.com/Moq/moq4)
- Lib DotNet.Core.Runtime.Faker.Moq

Muito parecido com o FakeItEasy, mas com a sintaxe do moq
```c#
factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(mock => mock.Setup(x => x.Now()).Returns(registeredValue));
}));
```
e
```c#
 serviceProvider.Change<Clock>(mock => mock.Setup(x => x.Now()).Returns(new DateTime()));
```

### Manualmente
- Lib DotNet.Core.Runtime.Faker

Muito parecido com os anteriores, mas sem dependências das libs
```c#
factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
builder.ConfigureTestServices(services =>
{
    services.AddServiceWithFaker<Clock>(() => new FakeClock());
}));
```
e
```c#
serviceProvider.Change<Clock>(() => new FakeClock());
```

Exemplos completos:
- [Com FakeItEasy](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingFakeItEasyTests.cs)
- [Com Moq](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingMoqTests.cs)
- [Manualmente](https://github.com/Lambda3/DotNet.Core.Runtime.Faker/blob/main/test/DotNet.Core.Runtime.Faker.Integration.Tests/RuntimeFakerUsingCustomFakerTests.cs)


