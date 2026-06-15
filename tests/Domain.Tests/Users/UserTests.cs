using School.Domain.Models.Users;

namespace Domain.Tests.Users;

public class UserTests
{
    [Fact]
    public void Register_ShouldCreateUser_WhenValidParametersAreProvided()
    {
        var name = FullName.Create("John", "Doe");
        var email = Email.Create("John@gmail.com");

        var user = User.Register(name, email, "password");

        Assert.True(user != null && user.Status == UserStatuses.Active && user.Role == GlobalRoles.User);
    }

    [Fact]
    public void Block_ShouldChangeStatusToBlocked_WhenUserIsActive()
    {
        var name = FullName.Create("John", "Doe");
        var email = Email.Create("John@gmail.com");
        var user = User.Register(name, email, "password");

        user.Block();

        Assert.Equal(UserStatuses.Blocked, user.Status);
    }

    [Fact]
    public void Unblock_ShouldChangeStatusToActive_WhenUserIsBlocked()
    {
        var name = FullName.Create("John", "Doe");
        var email = Email.Create("John@gmail.com");
        var user = User.Register(name, email, "password");

        user.Block();

        user.Unblock();

        Assert.Equal(UserStatuses.Active, user.Status);
    }
}