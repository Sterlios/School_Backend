using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using School.Application.Interfaces;
using School.Infrastructure.Databases;
using School.Infrastructure.Databases.Repositories;
using School.Infrastructure.Security;

namespace School.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IUserRepository, UsersRepository>()
            .AddScoped<IPasswordHasher, PasswordHasher>();

        builder.Services.AddDbContext<PostgreSQLContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

        return builder;
    }
}
