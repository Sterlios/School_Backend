using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Users;
using School.Domain.Users.Roles;

namespace School.Infrastructure.Databases.Configurations;

public static class UsersTableConfiguration
{
    public static void Configure(this EntityTypeBuilder<User> builder)
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

        builder.Property(u => u.RoleId)
            .HasColumnName("RoleId")
            .IsRequired();

        builder.HasOne<GlobalRole>()
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
