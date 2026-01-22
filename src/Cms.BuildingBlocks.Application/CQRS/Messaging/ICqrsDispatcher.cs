using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Queries;

namespace Cms.BuildingBlocks.Application.CQRS.Messaging;

public interface ICqrsDispatcher
{
    Task<TResponse> SendCommandAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken ct = default);

    Task<TResponse> SendQueryAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken ct = default);
}
