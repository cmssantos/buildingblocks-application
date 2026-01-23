using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Application.Core.Persistence;

public interface IUserOwnedRepository<TAggregate, TId, TOwnerId>
    : IRepository<TAggregate, TId>
    where TAggregate : OwnedAggregateRoot<TId, TOwnerId>
    where TId : IEntityId
    where TOwnerId : IEntityId
{
    Task<TAggregate?> GetByIdAsync(TId id, TOwnerId ownerId, CancellationToken ct);
    Task<IEnumerable<TAggregate>> ListAsync(TOwnerId ownerId, CancellationToken ct);
}
