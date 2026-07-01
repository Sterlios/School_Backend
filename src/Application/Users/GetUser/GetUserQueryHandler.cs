using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Users.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository)
{
    public async Task<GetUserResponse> Handle(GetUserQuery getUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(getUserQuery.Id), ct);

        if (user is null)
        {
            throw new Exception($"User with id {getUserQuery.Id} not found.");
        }

        return new GetUserResponse
        {
            Id = user.Id.Value,
            Name = string.Join(" ", user.Name.FirstName, user.Name.LastName),
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            Status = user.Status
        };
    }
}