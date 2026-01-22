# Cms.BuildingBlocks.Application

Reusable **Application Building Blocks** for .NET projects using **Clean Architecture**, **DDD**, and optional **CQRS** patterns.

This library provides:

- Core utilities: `Result`, `DomainError`, `Guard`, `Normalizer`, `IDateTimeProvider`
- Persistence abstractions: `IRepository<T>`, `IUnitOfWork`
- Event dispatching: `IDomainEvent`, `IDomainEventDispatcher`
- Optional CQRS layer: `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`, `ICqrsDispatcher`

## Project Structure

```
Cms.BuildingBlocks.Application
├─ Core
│   ├─ Common
│   │   ├─ Result.cs
│   │   ├─ DomainError.cs
│   │   ├─ Guard.cs
│   │   └─ Normalizer.cs
│   ├─ Persistence
│   │   ├─ IRepository<T>.cs
│   │   └─ IUnitOfWork.cs
│   ├─ Events
│   │   ├─ IDomainEvent.cs
│   │   └─ IDomainEventDispatcher.cs
│   └─ Utils
│       └─ DateTimeProvider.cs
├─ CQRS
│   ├─ Commands
│   │   ├─ ICommand.cs
│   │   └─ ICommandHandler.cs
│   ├─ Queries
│   │   ├─ IQuery.cs
│   │   └─ IQueryHandler.cs
│   └─ Messaging
│       ├─ ICqrsDispatcher.cs
│       ├─ CqrsDispatcher.cs
│       └─ CqrsDispatcherExtensions.cs
```

## Installation

Add the NuGet package (after publishing):

```bash
dotnet add package Cms.BuildingBlocks.Application
```

Or via PackageReference:

```xml
<PackageReference Include="Cms.BuildingBlocks.Application" Version="x.y.z" />
```

## Usage

### Core

```csharp
using Cms.BuildingBlocks.Application.Core.Common;

// Normalizing strings
var normalized = Normalizer.TrimAndCollapseSpaces("  hello   world  "); // "hello world"

// Guard clauses
Guard.AgainstNullOrEmpty(normalized, nameof(normalized));

// Using Result<T>
var success = Result<int>.Success(42);
var failure = Result<int>.Failure(new DomainError("Something went wrong"));
```

### Persistence

```csharp
using Cms.BuildingBlocks.Application.Core.Persistence;

public class MyService
{
    private readonly IRepository<MyAggregate> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public MyService(IRepository<MyAggregate> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task DoSomethingAsync()
    {
        var entity = new MyAggregate();
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
```

### CQRS (Optional)

```csharp
using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Messaging;

// Define command and handler
public record TestCommand(string Message) : ICommand<string>;

public class TestCommandHandler : ICommandHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand command, CancellationToken ct)
        => Task.FromResult($"Processed {command.Message}");
}

// Register dispatcher and handlers
var services = new ServiceCollection();
services.AddCqrsDispatcher(typeof(TestCommandHandler).Assembly);
var provider = services.BuildServiceProvider();

var dispatcher = provider.GetRequiredService<ICqrsDispatcher>();
var result = await dispatcher.SendCommandAsync(new TestCommand("Hello"));
```

## Contributing

- Follow **Clean Architecture** and **DDD principles**.
- Add tests using **xUnit + Shouldly**.
- Ensure all **Core and CQRS abstractions** are reusable and decoupled from infrastructure.

## License

MIT License

---
