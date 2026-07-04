using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Infrastructure.Databases;

public class PostgreSQLContext: DbContext, IUnitOfWork
{
    public PostgreSQLContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id)
                .HasName("PK_Users");

            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Id)
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value));

            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value))
                .HasColumnName("Email")
                .IsRequired();

            builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();
            });

            builder.Property(u => u.Role)
                .HasConversion(
                    v => (int)v,
                    v => (GlobalRoles)v);
        });
    }
}