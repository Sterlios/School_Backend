
using School.Domain.Users;

namespace School.Application.Interfaces;

public interface IRolesRepository
{
    Task<GlobalRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<GlobalRole>> GetAllByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
    Task<GlobalRole> GetDefaultAsync(CancellationToken cancellationToken = default);
}