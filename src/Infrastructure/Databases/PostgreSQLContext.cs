using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Users;
using School.Domain.Users.Roles;
using School.Infrastructure.Databases.Configurations;

namespace School.Infrastructure.Databases;

public class PostgreSQLContext: DbContext, IUnitOfWork
{
    public PostgreSQLContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<GlobalRole> GlobalRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder => builder.Configure());
        modelBuilder.Entity<GlobalRole>(builder => builder.Configure());
    }
}