namespace Cms.BuildingBlocks.Application.CQRS.Queries;

public interface IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(
        TQuery query,
        CancellationToken ct);
}
