using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Infrastructure.Databases.Repositories;

public class RolesRepository(PostgreSQLContext context): IRolesRepository
{
    public async Task<List<GlobalRole>> GetAllByIdsAsync(List<int> ids, CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(cancellationToken);

    public async Task<GlobalRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<GlobalRole> GetDefaultAsync(CancellationToken cancellationToken = default) =>
        await context.GlobalRoles
            .FirstOrDefaultAsync(r => r.IsDefault, cancellationToken)
            ?? await context.GlobalRoles.FirstAsync(cancellationToken);
}
