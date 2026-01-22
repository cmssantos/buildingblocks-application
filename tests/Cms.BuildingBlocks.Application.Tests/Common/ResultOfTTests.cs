using Cms.BuildingBlocks.Application.Core.Common;
using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.Common;

public class ResultOfTTests
{
    [Fact]
    public void Success_ShouldReturnSuccessResult()
    {
        var result = Result.Success();
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Error.ShouldBeNull();
        result.HasError.ShouldBeFalse();
    }

    [Fact]
    public void Failure_ShouldReturnFailureResult()
    {
        var error = new DomainError("Something went wrong");
        var result = Result.Failure(error);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
        result.HasError.ShouldBeTrue();
    }

    [Fact]
    public void Success_ShouldReturnCorrectProperties()
    {
        var value = 123;
        var result = Result<int>.Success(value);

        // Propriedades principais
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.HasError.ShouldBeFalse();
        result.Value.ShouldBe(value);
        result.Error.ShouldBeNull();
    }

    [Fact]
    public void Failure_ShouldReturnCorrectProperties()
    {
        var error = new DomainError("Something went wrong");
        var result = Result<int>.Failure(error);

        // Propriedades principais
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.HasError.ShouldBeTrue();
        result.Value.ShouldBe(default); // valor padrão
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void AccessingAllProperties_EnsuresCoverage()
    {
        var value = "hello";
        var success = Result<string>.Success(value);
        _ = success.IsSuccess;
        _ = success.IsFailure;
        _ = success.HasError;
        _ = success.Value;
        _ = success.Error;

        var error = new DomainError("fail");
        var failure = Result<string>.Failure(error);
        _ = failure.IsSuccess;
        _ = failure.IsFailure;
        _ = failure.HasError;
        _ = failure.Value;
        _ = failure.Error;
    }
}
