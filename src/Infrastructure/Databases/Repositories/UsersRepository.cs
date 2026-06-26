using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Infrastructure.Databases.Repositories;

public class UsersRepository(PostgreSQLContext context): IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken) =>
        await context.AddAsync(user);

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken) =>
        await context.Users.AnyAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken ct) =>
        await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
}
