using System.Reflection;
using Application.Common;
using Application.Common.Behaviourspipe;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheQueryBehavior<,>));
    }
}