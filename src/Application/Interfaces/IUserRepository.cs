using School.Application.Users.Requests;
using School.Domain.Users;

namespace School.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(Email email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<List<User>> GetUsersAsync(FilterUsersListRequest filterUsersListQuery, CancellationToken cancellationToken = default);
}
