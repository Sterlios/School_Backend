using FluentAssertions;
using School.Domain.Users;

namespace School.Domain.Tests.Users;

public class UserTests
{
    [Fact]
    public void Register_Should_CreateUser_When_ValidParametersAreProvided()
    {
        var user = Create();

        user.Should().NotBeNull();
        user.Status.Should().Be(UserStatuses.Active);
        user.Role.Should().Be(GlobalRoles.User);
    }

    [Fact]
    public void Register_Should_Throw_When_NameIsNull()
    {
        Action act = () => User.Register(null!, Email.Create("Anton@gmail.com"), "password");

        act.Should().Throw();
    }

    [Fact]
    public void Register_Should_Throw_When_EmailIsNull()
    {
        Action act = () => User.Register(FullName.Create("Anton", "Kuzmin"), null, "password");

        act.Should().Throw();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Register_Should_Throw_When_PasswordIsNullOrEmptyOrWhiteSpace(string password)
    {
        Action act = () => User.Register(FullName.Create("Anton", "Kuzmin"), Email.Create("Anton@gmail.com"), password);

        act.Should().Throw();
    }

    [Fact]
    public void Block_Should_ChangeStatusToBlocked_When_UserIsActive()
    {
        var user = Create();

        user.Block();

        user.Status.Should().Be(UserStatuses.Blocked);
    }

    [Fact]
    public void Block_Should_Throw_When_UserIsAlreadyBlocked()
    {
        var user = Create();

        user.Block();

        Action act = user.Block;

        act.Should().Throw();
    }

    [Fact]
    public void Unblock_Should_ChangeStatusToActive_When_UserIsBlocked()
    {
        var user = Create();

        user.Block();

        user.Unblock();

        user.Status.Should().Be(UserStatuses.Active);
    }

    [Fact]
    public void Unblock_Should_Throw_When_UserIsAlreadyActive()
    {
        var user = Create();

        Action act = user.Unblock;

        act.Should().Throw();
    }

    [Fact]
    public void ChangeRoleToAdmin_Should_ChangeRole_When_UserIsUser()
    {
        var user = Create();

        user.ChangeRole(GlobalRoles.Admin);

        user.Role.Should().Be(GlobalRoles.Admin);
    }

    [Fact]
    public void ChangeRoleToUser_Should_Throw_When_UserIsUser()
    {
        var user = Create();

        Action act = () => user.ChangeRole(GlobalRoles.User);

        act.Should().Throw();
    }

    [Fact]
    public void ChangePassword_Should_Change_When_NewPasswordIsOk()
    {
        var user = Create();

        user.ChangePassword("newPassword");

        user.PasswordHash.Should().Be("newPassword");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangePassword_Should_Throw_When_NewPasswordIsNullOrEmptyOrWhiteSpace(string password)
    {
        var user = Create();

        Action act = () => user.ChangePassword(password);

        act.Should().Throw();
    }

    [Fact]
    public void ChangePassword_Should_Throw_When_NewPasswordEqualOldPassword()
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