using FluentAssertions;
using Moq;
using School.Application.Authorization;
using School.Application.Interfaces;
using School.Application.Users;
using School.Application.Users.Queries;
using School.Domain.Users;

namespace School.Application.Tests.Users;

public class RegisterUserHandlerTests
{
    [Fact]
    public async Task Handle_Should_RegisterUser_When_EmailIsFree()
    {
        var repository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        var rolesRepository = new Mock<IRolesRepository>();

        repository
            .Setup(x => x.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("hash");

        jwtTokenGenerator
            .Setup(x => x.Generate(It.IsAny<UserPayload>()))
            .Returns("token");

        var userService = new UserService(repository.Object, rolesRepository.Object, passwordHasher.Object, unitOfWork.Object, jwtTokenGenerator.Object);

        var command = new RegisterUserCommand()
        {
            FirstName = "Anton",
            LastName = "Kuzmin",
            Email = "anton@gmail.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        var result = await userService.Register(command, CancellationToken.None);

        result.Should().NotBeNull();

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_EmailExists()
    {
        var repository = new Mock<IUserRepository>();
        var rolesRepository = new Mock<IRolesRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var jwtTokenGenerator = new Mock<IJwtTokenGenerator>();

        repository
            .Setup(x => x.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("hash");

        jwtTokenGenerator
            .Setup(x => x.Generate(It.IsAny<UserPayload>()))
            .Returns("token");

        var userService = new UserService(repository.Object, rolesRepository.Object, passwordHasher.Object, unitOfWork.Object, jwtTokenGenerator.Object);

        var command = new RegisterUserCommand()
        {
            FirstName = "Anton",
            LastName = "Kuzmin",
            Email = "anton@gmail.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        var act = () => userService.Register(command, CancellationToken.None);

        await act.Should().ThrowAsync();
    }
}