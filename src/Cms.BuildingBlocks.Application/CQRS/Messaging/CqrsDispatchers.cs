using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Queries;

using Microsoft.Extensions.DependencyInjection;

namespace Cms.BuildingBlocks.Application.CQRS.Messaging;

public sealed class CqrsDispatcher(
    IServiceProvider serviceProvider)
    : ICqrsDispatcher
{
    public async Task<TResponse> SendCommandAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        Type handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(command.GetType(), typeof(TResponse));

        dynamic handler = serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle((dynamic)command, ct);
    }

    public async Task<TResponse> SendQueryAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        Type handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResponse));

        dynamic handler = serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle((dynamic)query, ct);
    }
}
