using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Application.Core.Persistence;

public interface IRepository<TAggregate>
    where TAggregate
        : AggregateRoot<IEntityId>
{
    Task AddAsync(TAggregate aggregate, CancellationToken ct = default);
    Task UpdateAsync(TAggregate aggregate, CancellationToken ct = default);
    Task<TAggregate?> GetByIdAsync(IEntityId id, CancellationToken ct = default);
    Task<IEnumerable<TAggregate>> ListAsync(CancellationToken ct = default);
}
