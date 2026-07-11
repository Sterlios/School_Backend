using School.Domain.Users.Roles;

namespace School.Application.Interfaces;

public interface IRolesRepository
{
    Task<GlobalRole?> GetByIdAsync(GlobalRoleId id, CancellationToken cancellationToken = default);
    Task<List<GlobalRole>> GetAllByIdsAsync(List<GlobalRoleId> ids, CancellationToken cancellationToken = default);
    Task<GlobalRole> GetDefaultAsync(CancellationToken cancellationToken = default);
}