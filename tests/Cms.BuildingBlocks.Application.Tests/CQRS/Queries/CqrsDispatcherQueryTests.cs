using Cms.BuildingBlocks.Application.CQRS.Messaging;
using Cms.BuildingBlocks.Application.CQRS.Queries;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.CQRS.Queries;

public class TestQuery : IQuery<string>
{
    public string Input { get; init; } = "";
}

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public Task<string> Handle(TestQuery query, CancellationToken ct)
        => Task.FromResult($"Queried {query.Input}");
}

public class CqrsDispatcherQueryTests
{
    [Fact]
    public async Task SendQueryAsync_ShouldCallHandler()
    {
        var services = new ServiceCollection();

        services.AddTransient<IQueryHandler<TestQuery, string>, TestQueryHandler>();
        services.AddSingleton<ICqrsDispatcher, CqrsDispatcher>();

        ServiceProvider provider = services.BuildServiceProvider();

        ICqrsDispatcher dispatcher = provider.GetRequiredService<ICqrsDispatcher>();

        var query = new TestQuery { Input = "World" };
        var result = await dispatcher.SendQueryAsync(query);

        result.ShouldBe("Queried World");
    }
}
