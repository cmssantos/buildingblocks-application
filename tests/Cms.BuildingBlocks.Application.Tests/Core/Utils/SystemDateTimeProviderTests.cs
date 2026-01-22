using Cms.BuildingBlocks.Application.Core.Utils;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.Core.Utils;

public class SystemDateTimeProviderTests
{
    [Fact]
    public void UtcNow_ShouldReturnCurrentUtcTime()
    {
        var provider = new SystemDateTimeProvider();
        DateTime now = DateTime.UtcNow;

        DateTime providerNow = provider.UtcNow;

        // Ensure the provider's time is very close to the actual current time
        (providerNow - now).TotalSeconds.ShouldBeLessThan(1);
    }
}
