using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Messaging;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using System.Reflection;

using Xunit;

namespace Cms.BuildingBlocks.Application.Tests.CQRS.Messaging;

public class DummyCommand : ICommand<string>
{
    public string Msg { get; init; } = "";
}

public class DummyCommandHandler : ICommandHandler<DummyCommand, string>
{
    public Task<string> Handle(DummyCommand command, CancellationToken ct)
        => Task.FromResult($"Handled {command.Msg}");
}

public class CqrsDispatcherRegistrationTests
{
    [Fact]
    public async Task AddCqrsDispatcher_ShouldRegisterHandlersFromAssembly()
    {
        var services = new ServiceCollection();

        services.AddCqrsDispatcher(Assembly.GetExecutingAssembly());
        ServiceProvider provider = services.BuildServiceProvider();

        ICqrsDispatcher dispatcher = provider.GetRequiredService<ICqrsDispatcher>();
        dispatcher.ShouldNotBeNull();

        var cmd = new DummyCommand { Msg = "Test" };
        var result = await dispatcher.SendCommandAsync(cmd);

        result.ShouldBe("Handled Test");
    }
}
