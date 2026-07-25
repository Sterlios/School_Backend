using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Authorization;
using School.Application.Users;

namespace School.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<AuthService>();

        return builder;
    }
}
