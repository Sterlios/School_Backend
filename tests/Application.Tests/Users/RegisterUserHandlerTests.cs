using FluentAssertions;
using Moq;
using School.Application.Authorization;
using School.Application.Authorization.Requests;
using School.Application.Interfaces;
using School.Domain.Users;
using School.Domain.Users.Roles;

namespace School.Application.Tests.Users;

public class RegisterUserHandlerTests
{
    [Fact]
    public async Task Register_Should_RegisterUser_When_EmailIsFree()
    {
        var repository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var rolesRepository = new Mock<IRolesRepository>();

        repository
            .Setup(x => x.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        rolesRepository
            .Setup(x => x.GetDefaultAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GlobalRole()
            {
                Id = new GlobalRoleId(1),
                Name = "User",
                IsDefault = true
            });

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("hash");

        var authService = new AuthService(repository.Object, rolesRepository.Object, passwordHasher.Object, unitOfWork.Object);

        var command = new RegisterUserRequest(
            "Anton",
            "Kuzmin",
            "anton@gmail.com",
            "password",
            "password"
        );

        var result = await authService.Register(command, It.IsAny<CancellationToken>());

        result.Should().NotBeNull();

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Register_Should_Throw_When_EmailExists()
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
            .Setup(x => x.Generate(It.IsAny<UserJwtPayload>()))
            .Returns("token");

        var authService = new AuthService(repository.Object, rolesRepository.Object, passwordHasher.Object, unitOfWork.Object);

        var command = new RegisterUserRequest(
            "Anton",
            "Kuzmin",
            "anton@gmail.com",
            "password",
            "password"
        );

        var act = () => authService.Register(command, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}