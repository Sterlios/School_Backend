using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Users.BlockUser;
using School.Application.Users.GetUser;
using School.Application.Users.GetUsersList;
using School.Application.Users.LoginUser;
using School.Application.Users.RegisterUser;
using School.Application.Users.UnblockUser;

namespace School.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<RegisterUserCommandHandler>();
        builder.Services.AddScoped<GetUserQueryHandler>();
        builder.Services.AddScoped<LoginUserCommandHandler>();
        builder.Services.AddScoped<GetUsersListHandler>();
        builder.Services.AddScoped<UnblockUserHandler>();
        builder.Services.AddScoped<BlockUserHandler>();

        return builder;
    }
}
