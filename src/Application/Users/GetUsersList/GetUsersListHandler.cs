using School.Application.Interfaces;

namespace School.Application.Users.GetUsersList;

public class GetUsersListHandler(IUserRepository userRepository)
{
    public async Task<List<GetUserResponse>> Handle(FilterUsersListQuery filterUsersListQuery, CancellationToken ct)
    {
        var users = await userRepository.GetUsersAsync(filterUsersListQuery, ct);

        return users.Select(u => new GetUserResponse
        {
            Id = u.Id.Value,
            Name = string.Join(" ", u.Name.FirstName, u.Name.LastName),
            Email = u.Email.Value
        }).ToList();
    }
}
