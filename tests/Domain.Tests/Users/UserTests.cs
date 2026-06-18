using FluentAssertions;
using School.Domain.Users;

namespace Domain.Tests.Users;

public class UserTests
{
    [Fact]
    public void Register_Should_CreateUser_WhenValidParametersAreProvided()
    {
        var user = Create();

        user.Should().NotBeNull();
        user.Status.Should().Be(UserStatuses.Active);
        user.Role.Should().Be(GlobalRoles.User);
    }

    [Fact]
    public void Register_Should_ThrowException_WhenNameIsNull()
    {
        Action act = () => User.Register(null!, Email.Create("Anton@gmail.com"), "password");

        act.Should().Throw();
    }

    [Fact]
    public void Register_Should_ThrowException_WhenEmailIsNull()
    {
        Action act = () => User.Register(FullName.Create("Anton", "Kuzmin"), null, "password");

        act.Should().Throw();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Register_Should_ThrowException_WhenPasswordIsNullOrEmptyOrWhiteSpace(string password)
    {
        Action act = () => User.Register(FullName.Create("Anton", "Kuzmin"), Email.Create("Anton@gmail.com"), password);

        act.Should().Throw();
    }

    [Fact]
    public void Block_Should_ChangeStatusToBlocked_WhenUserIsActive()
    {
        var user = Create();

        user.Block();

        user.Status.Should().Be(UserStatuses.Blocked);
    }

    [Fact]
    public void Block_Should_ThrowException_WhenUserIsAlreadyBlocked()
    {
        var user = Create();

        user.Block();

        Action act = user.Block;

        act.Should().Throw();
    }

    [Fact]
    public void Unblock_Should_ChangeStatusToActive_WhenUserIsBlocked()
    {
        var user = Create();

        user.Block();

        user.Unblock();

        user.Status.Should().Be(UserStatuses.Active);
    }

    [Fact]
    public void Unblock_Should_ThrowException_WhenUserIsAlreadyActive()
    {
        var user = Create();

        Action act = user.Unblock;

        act.Should().Throw();
    }

    [Fact]
    public void ChangeRoleToAdmin_Should_ChangeRole_WhenUserIsUser()
    {
        var user = Create();

        user.ChangeRole(GlobalRoles.Admin);

        user.Role.Should().Be(GlobalRoles.Admin);
    }

    [Fact]
    public void ChangeRoleToUser_Should_ThrowException_WhenUserIsUser()
    {
        var user = Create();

        Action act = () => user.ChangeRole(GlobalRoles.User);

        act.Should().Throw();
    }

    [Fact]
    public void ChangePassword_Should_Change_WhenNewPasswordIsOk()
    {
        var user = Create();

        user.ChangePassword("newPassword");

        user.PasswordHash.Should().Be("newPassword");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangePassword_Should_ThrowException_WhenNewPasswordIsNullOrEmptyOrWhiteSpace(string password)
    {
        var user = Create();

        Action act = () => user.ChangePassword(password);

        act.Should().Throw();
    }

    [Fact]
    public void ChangePassword_Should_ThrowException_WhenNewPasswordEqualOldPassword()
    {
        var user = Create();

        Action act = () => user.ChangePassword(user.PasswordHash);

        act.Should().Throw();
    }

    private User Create()
    {
        var name = FullName.Create("Anton", "Kuzmin");
        var email = Email.Create("Anton@gmail.com");

        return User.Register(name, email, "password");
    }
}