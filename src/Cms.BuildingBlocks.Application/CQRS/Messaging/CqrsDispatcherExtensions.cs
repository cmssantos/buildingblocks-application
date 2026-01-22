using System.Reflection;

using Cms.BuildingBlocks.Application.CQRS.Commands;
using Cms.BuildingBlocks.Application.CQRS.Queries;

using Microsoft.Extensions.DependencyInjection;

namespace Cms.BuildingBlocks.Application.CQRS.Messaging;

public static class CqrsDispatcherExtensions
{
    public static IServiceCollection AddCqrsDispatcher(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddSingleton<ICqrsDispatcher, CqrsDispatcher>();

        foreach (Assembly assembly in assemblies)
        {
            var commandHandlers = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                .ToList();

            foreach (Type? handler in commandHandlers)
            {
                Type interfaceType = handler.GetInterfaces()
                    .First(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));

                services.AddTransient(interfaceType, handler);
            }

            var queryHandlers = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .ToList();

            foreach (Type? handler in queryHandlers)
            {
                Type interfaceType = handler.GetInterfaces()
                    .First(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                services.AddTransient(interfaceType, handler);
            }
        }

        return services;
    }
}
