using Microsoft.Extensions.DependencyInjection;

namespace School.Domain.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}
