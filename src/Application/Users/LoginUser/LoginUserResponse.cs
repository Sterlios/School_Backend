using School.Domain.Users;

namespace School.Application.Users.LoginUser;

public class LoginUserResponse
{
    public UserId? UserId { get; set; }
    public string? ErrorStatus { get; set; }
}
