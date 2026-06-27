using School.Domain.Users;

namespace School.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<User?> GetByEmail(Email email);
    Task<User?> GetByIdAsync(UserId id, CancellationToken ct);
}
