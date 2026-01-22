using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Messaging;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.CQRS.Commands;

public class TestCommand : ICommand<string>
{
    public string Input { get; init; } = "";
}

public class TestCommandHandler : ICommandHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand command, CancellationToken ct)
        => Task.FromResult($"Processed {command.Input}");
}

public class CqrsDispatcherCommandTests
{
    [Fact]
    public async Task SendCommandAsync_ShouldCallHandler()
    {
        var services = new ServiceCollection();

        services.AddTransient<ICommandHandler<TestCommand, string>, TestCommandHandler>();
        services.AddSingleton<ICqrsDispatcher, CqrsDispatcher>();

        ServiceProvider provider = services.BuildServiceProvider();

        ICqrsDispatcher dispatcher = provider.GetRequiredService<ICqrsDispatcher>();

        var command = new TestCommand { Input = "Hello" };
        var result = await dispatcher.SendCommandAsync(command);

        result.ShouldBe("Processed Hello");
    }
}
