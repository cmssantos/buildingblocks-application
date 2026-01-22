using Cms.BuildingBlocks.Application.Core.Common;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.Core.Common;

public class NormalizerTests
{
    [Theory]
    [InlineData("   hello world  ", "hello world")]
    [InlineData("a   b   c", "a b c")]
    [InlineData(" singleword ", "singleword")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    public void TrimAndCollapseSpaces_ShouldNormalizeStringsCorrectly(string input, string expected)
    {
        var result = Normalizer.TrimAndCollapseSpaces(input);
        result.ShouldBe(expected);
    }
}

