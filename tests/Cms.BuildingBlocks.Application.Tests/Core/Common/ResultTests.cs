using Cms.BuildingBlocks.Application.Core.Common;
using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.Core.Common;

public class ResultTests
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
}
