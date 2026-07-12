using School.Application.Interfaces;
using School.Application.Users.Requests;
using School.Application.Users.Responses;
using School.Domain.Users;

namespace School.Application.Users;

public class UserService(
    IUserRepository userRepository,
    IRolesRepository rolesRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<List<GetUserResponse>> GetUsersList(FilterUsersListRequest filterUsersListRequest, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(filterUsersListRequest, cancellationToken);

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

    public async Task<GetUserResponse> GetUser(GetUserRequest getUserRequest, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(new UserId(getUserRequest.Id), cancellationToken);

        var role = await rolesRepository.GetByIdAsync(user.RoleId, cancellationToken);

        if (user is null)
        {
            throw new Exception($"User with id {getUserRequest.Id} not found.");
        }

        return new GetUserResponse(
            Id: user.Id.Value,
            Name: string.Join(" ", user.Name.FirstName, user.Name.LastName),
            Email: user.Email.Value,
            Role: role?.Name ?? "Unknown",
            Status: user.Status.ToString());
    }

    public async Task BlockUser(BlockUserRequest blockUserRequest, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(new UserId(blockUserRequest.Id), cancellationToken);

        if (user is null)
            throw new Exception($"User with id {blockUserRequest.Id} not found.");

        user.Block();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnblockUser(UnblockUserRequest unblockUserRequest, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(new UserId(unblockUserRequest.id), cancellationToken);

        if (user is null)
            throw new Exception($"User with id {unblockUserRequest.id} not found.");

        user.Unblock();

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
