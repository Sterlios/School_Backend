using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Users.UnblockUser;

public class UnblockUserHandler(IUserRepository userRepository)
{
    public async Task Handle(UnblockUserQuery blockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(blockUserQuery.Id), ct);

        if (user is null)
            throw new Exception($"User with id {blockUserQuery.Id} not found.");

        user.Unblock();
    }
}
