using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Application.Users.Requests;
using School.Domain.Users;

namespace School.Infrastructure.Databases.Repositories;

public class UsersRepository(PostgreSQLContext context): IUserRepository
{
    public void Add(User user) =>
        context.Add(user);

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
        await context.Users.AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByEmail(Email email, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<List<User>> GetUsersAsync(FilterUsersListRequest filterUsersListQuery, CancellationToken cancellationToken = default) =>
        await context.Users
            .Skip((filterUsersListQuery.PageNumber - 1) * filterUsersListQuery.PageSize)
            .Take(filterUsersListQuery.PageSize)
            .ToListAsync(cancellationToken);
}
