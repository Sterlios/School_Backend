using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Users.Roles;

namespace School.Infrastructure.Databases.Configurations;

public static class PermissionsTableConfiguration
{
    public static void Configure(this EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(r => r.Id)
            .HasName("PK_Permissions");

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Id,
                value => new PermissionId(value))
            .HasIdentityOptions(1, 1);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
