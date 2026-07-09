using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Application.Users.Queries;
using School.Domain.Users;

namespace School.Infrastructure.Databases.Repositories;

public class UsersRepository(PostgreSQLContext context): IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await context.AddAsync(user, cancellationToken);

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
        await context.Users.AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByEmail(Email email, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<List<User>> GetUsersAsync(FilterUsersListQuery filterUsersListQuery, CancellationToken cancellationToken = default) =>
        await context.Users
            .Skip((filterUsersListQuery.PageNumber - 1) * filterUsersListQuery.TotalCount)
            .Take(filterUsersListQuery.TotalCount)
            .ToListAsync(cancellationToken);
}
