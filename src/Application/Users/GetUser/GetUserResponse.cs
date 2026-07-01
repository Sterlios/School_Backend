using School.Domain.Users;

namespace School.Application.Users.GetUser;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public UserStatuses Status { get; set; }
}
