using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Users.GetUser;
using School.Application.Users.RegisterUser;

namespace School.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<RegisterUserCommandHandler>();
        builder.Services.AddScoped<GetUserQueryHandler>();

        return builder;
    }
}
