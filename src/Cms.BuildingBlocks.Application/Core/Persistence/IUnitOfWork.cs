namespace Cms.BuildingBlocks.Application.Core.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct = default);
}
