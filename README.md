# Cms.BuildingBlocks.Application

Reusable **Application Building Blocks** for .NET projects using **Clean Architecture**, **DDD**, and optional **CQRS** patterns.

[![CI](https://github.com/cmssantos/buildingblocks-application/actions/workflows/ci.yml/badge.svg)](https://github.com/cmssantos/buildingblocks-application/actions/workflows/ci.yml)
[![Release](https://github.com/cmssantos/buildingblocks-application/actions/workflows/release.yml/badge.svg)](https://github.com/cmssantos/buildingblocks-application/actions/workflows/release.yml)
![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Application)
![Downloads](https://img.shields.io/nuget/dt/Cms.BuildingBlocks.Application)
![License](https://img.shields.io/badge/license-MIT-blue)

---

## 🎯 Purpose

This package provides **application-layer abstractions** focused on
**use cases orchestration**, **business flow control**, and
**infrastructure decoupling**, without leaking technical concerns
into the Domain layer.

It is designed to be reusable across **monoliths, modular monoliths,
and microservices**.

---

## ✨ What This Library Provides

- Core utilities:
  - `Result`, `DomainError`
  - `Guard`, `Normalizer`
  - `IDateTimeProvider`
- Persistence abstractions:
  - `IRepository<TAggregate, TId>`
  - `IUserOwnedRepository<TAggregate, TId, TOwnerId>`
  - `IUnitOfWork`
- Event dispatching:
  - `IDomainEvent`
  - `IDomainEventDispatcher`
- Optional CQRS layer:
  - `ICommand`, `IQuery`
  - `ICommandHandler`, `IQueryHandler`
  - `ICqrsDispatcher`

---

## 📦 Installation

```bash
dotnet add package Cms.BuildingBlocks.Application
```

Or via `PackageReference`:

```xml
<PackageReference Include="Cms.BuildingBlocks.Application" Version="x.y.z" />
```

---

## 🧩 Usage

### Core Utilities

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

---

### Persistence — Global Aggregate

Use `IRepository` for aggregates that are **not scoped to a specific owner**.

```csharp
using Cms.BuildingBlocks.Application.Core.Persistence;

public class CreateSystemSettingHandler
{
    private readonly IRepository<SystemSetting, SystemSettingId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSystemSettingHandler(
        IRepository<SystemSetting, SystemSettingId> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SystemSetting setting, CancellationToken ct)
    {
        await _repository.AddAsync(setting, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
```

---

### Persistence — User / Tenant Owned Aggregate

Use `IUserOwnedRepository` when the aggregate lifecycle and visibility
are bound to a **user**, **account**, or **tenant**.

```csharp
using Cms.BuildingBlocks.Application.Core.Persistence;

public class DeleteCategoryHandler
{
    private readonly IUserOwnedRepository<Category, CategoryId, UserId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(
        IUserOwnedRepository<Category, CategoryId, UserId> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        CategoryId categoryId,
        UserId userId,
        CancellationToken ct)
    {
        var category = await _repository.GetByIdAsync(categoryId, userId, ct);
        if (category is null)
            throw new NotFoundException(nameof(Category), categoryId.Value);

        await _repository.DeleteAsync(category, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
```

---

### CQRS (Optional)

```csharp
using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Messaging;

public record TestCommand(string Message) : ICommand<string>;

public class TestCommandHandler
    : ICommandHandler<TestCommand, string>
{
    public Task<string> Handle(
        TestCommand command,
        CancellationToken ct)
        => Task.FromResult($"Processed {command.Message}");
}
```

---

## 🧭 Naming Guidelines (Important)

This library intentionally uses **`OwnerId`** instead of `UserId`
in generic abstractions.

### Why `OwnerId`?

- Works for **User-based systems**
- Works for **Tenant-based SaaS**
- Works for **Account / Organization ownership**
- Avoids coupling abstractions to authentication models

👉 In concrete services:
- Use `UserId`, `TenantId`, `AccountId`, etc.
- Bind them to `TOwnerId`

Example:

```csharp
IUserOwnedRepository<Category, CategoryId, UserId>
IUserOwnedRepository<Project, ProjectId, TenantId>
```

This maximizes **reuse without semantic leakage**.

---

## 🤝 Contributing

- Follow **Clean Architecture** and **DDD principles**
- Add tests using **xUnit + Shouldly**
- Keep Application abstractions free from infrastructure details
- Favor explicit contracts over implicit conventions

---

## 📜 License

MIT License
