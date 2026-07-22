using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Users.Roles;

namespace School.Infrastructure.Databases.Repositories;

public class RolesRepository(PostgreSQLContext context): IRolesRepository
{
    public async Task<List<GlobalRole>> GetAllByIdsAsync(List<GlobalRoleId> ids, CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(cancellationToken);

    public async Task<GlobalRole?> GetByIdAsync(GlobalRoleId id, CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .FirstOrDefaultAsync(r => r.Id.Equals(id), cancellationToken);

    public async Task<GlobalRole> GetDefaultAsync(CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .FirstOrDefaultAsync(r => r.IsDefault, cancellationToken)
            ?? await context.GlobalRoles.FirstAsync(cancellationToken);

    public Task<List<Permission>> GetPermissionsForRole(string roleName)
    {
        var role = context.GlobalRoles
            .Include(r => r.Permissions)
            .FirstOrDefault(r => r.Name == roleName);

        return Task.FromResult(role?.Permissions ?? new List<Permission>());
    }
}
