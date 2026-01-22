namespace Cms.BuildingBlocks.Application.CQRS.Commands;

public interface ICommandHandler<TCommand, TResponse>
       where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken ct);
}
