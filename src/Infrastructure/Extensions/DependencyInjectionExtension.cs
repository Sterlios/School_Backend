using Microsoft.Extensions.DependencyInjection;

namespace School.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
