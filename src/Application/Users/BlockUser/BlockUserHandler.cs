using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Users.BlockUser;

public class BlockUserHandler(IUserRepository userRepository)
{
    public async Task Handle(BlockUserQuery blockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(blockUserQuery.Id), ct);

        if (user is null)
            throw new Exception($"User with id {blockUserQuery.Id} not found.");

        user.Block();
    }
}
