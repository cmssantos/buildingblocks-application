# Cms.BuildingBlocks.Application

Reusable **Application Building Blocks** for .NET 10+ projects implementing **Clean Architecture**, **DDD**, and optional **CQRS**.

![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Application)
![Downloads](https://img.shields.io/nuget/dt/Cms.BuildingBlocks.Application)
![License](https://img.shields.io/badge/license-MIT-blue)

## Features

- **Core Utilities**: `Result`, `DomainError`, `Guard`, `Normalizer`, `IDateTimeProvider`
- **Persistence Abstractions**: `IRepository<T>`, `IUnitOfWork`
- **Domain Events**: `IDomainEvent`, `IDomainEventDispatcher`
- **Optional CQRS**: `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`, `ICqrsDispatcher`

## Installation

```bash
dotnet add package Cms.BuildingBlocks.Application
```

Or in `.csproj`:

```xml
<PackageReference Include="Cms.BuildingBlocks.Application" Version="x.y.z" />
```

## Quick Start

### Core Example

```csharp
var normalized = Normalizer.TrimAndCollapseSpaces("  hello   world  ");
Guard.AgainstNullOrEmpty(normalized, nameof(normalized));
var result = Result<int>.Success(42);
```

### CQRS Example

```csharp
public record TestCommand(string Msg) : ICommand<string>;
public class TestCommandHandler : ICommandHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand command, CancellationToken ct)
        => Task.FromResult($"Handled {command.Msg}");
}

// Register handlers via DI
services.AddCqrsDispatcher(typeof(TestCommandHandler).Assembly);

var dispatcher = provider.GetRequiredService<ICqrsDispatcher>();
var result = await dispatcher.SendCommandAsync(new TestCommand("Hello"));
```

## Compatibility

- .NET 10+
- Works in **microservices**, **monoliths**, and **domain-driven designs**

## License

MIT License
