using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Users.Roles;

namespace School.Infrastructure.Databases.Configurations;

public static class GlobalRolesTableConfiguration
{
    public static void Configure(this EntityTypeBuilder<GlobalRole> builder)
    {
        builder.ToTable("GlobalRoles");

        builder.HasKey(r => r.Id)
            .HasName("PK_GlobalRoles");

        builder.Property(r => r.Id)
            .HasConversion(
               id => id.Id,
                value => new GlobalRoleId(value))
            .HasIdentityOptions(1, 1);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(x => x.Permissions)
            .WithMany();
    }
}
