using System.Diagnostics.CodeAnalysis;

using Cms.BuildingBlocks.Domain.Errors;

namespace Cms.BuildingBlocks.Application.Core.Common;

public readonly struct Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    [MemberNotNullWhen(true, nameof(Error))]
    public bool HasError => IsFailure;

    public DomainError? Error { get; }

    private Result(bool success, DomainError? error)
    {
        IsSuccess = success;
        Error = error;
    }

    public static Result Success()
        => new(true, null);

    public static Result Failure(DomainError error)
        => new(false, error);
}

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    [MemberNotNullWhen(true, nameof(Error))]
    public bool HasError => IsFailure;

    public T Value { get; }

    public DomainError? Error { get; }

    private Result(bool success, T value, DomainError? error)
    {
        IsSuccess = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
        => new(true, value, null);

    public static Result<T> Failure(DomainError error)
        => new(false, default!, error);
}
