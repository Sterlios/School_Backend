using School.Application.Interfaces;
using School.Application.Users.Requests;
using School.Application.Users.Responses;
using School.Domain.Users;

namespace School.Application.Users;

public class UserService(
    IUserRepository userRepository,
    IRolesRepository rolesRepository)
{
    public async Task<List<GetUserResponse>> GetUsersList(FilterUsersListRequest filterUsersListQuery, CancellationToken ct)
    {
        var users = await userRepository.GetUsersAsync(filterUsersListQuery, ct);

        var roleIds = users
            .Select(u => u.RoleId)
            .Distinct()
            .ToList();

        var roles = await rolesRepository.GetAllByIdsAsync(roleIds);

        return users
            .Select(u => new GetUserResponse(
                Id: u.Id.Value,
                Name: string.Join(" ", u.Name.FirstName, u.Name.LastName),
                Email: u.Email.Value,
                Role: roles.FirstOrDefault(r => r.Id.Equals(u.RoleId))?.Name ?? "Unknown",
                Status: u.Status.ToString()))
            .ToList();
    }

    public async Task<GetUserResponse> GetUser(GetUserRequest getUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(getUserQuery.Id), ct);

        var role = await rolesRepository.GetByIdAsync(user.RoleId, ct);

        if (user is null)
        {
            throw new Exception($"User with id {getUserQuery.Id} not found.");
        }

        return new GetUserResponse(
            Id: user.Id.Value,
            Name: string.Join(" ", user.Name.FirstName, user.Name.LastName),
            Email: user.Email.Value,
            Role: role?.Name ?? "Unknown",
            Status: user.Status.ToString());
    }

    public async Task BlockUser(BlockUserRequest blockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(blockUserQuery.Id), ct);

        if (user is null)
            throw new Exception($"User with id {blockUserQuery.Id} not found.");

        user.Block();
    }

    public async Task UnblockUser(UnblockUserRequest unblockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(unblockUserQuery.id), ct);

        if (user is null)
            throw new Exception($"User with id {unblockUserQuery.id} not found.");

        user.Unblock();
    }
}
