namespace School.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddPermissionsProvider(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim("permission", "")));

        return services;
    }
}
