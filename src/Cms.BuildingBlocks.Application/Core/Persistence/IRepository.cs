using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Application.Core.Persistence;

public interface IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : IEntityId
{
    Task AddAsync(TAggregate aggregate, CancellationToken ct = default);
    Task UpdateAsync(TAggregate aggregate, CancellationToken ct = default);
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<IEnumerable<TAggregate>> ListAsync(CancellationToken ct = default);
}
