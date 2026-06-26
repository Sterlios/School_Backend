using FluentAssertions;
using Moq;
using School.Application.Interfaces;
using School.Application.Users.RegisterUser;
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

        repository
            .Setup(x => x.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("hash");

        var handler = new RegisterUserCommandHandler(repository.Object, passwordHasher.Object, unitOfWork.Object);

        var command = new RegisterUserCommand()
        {
            FirstName = "Anton",
            LastName = "Kuzmin",
            Email = "anton@gmail.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_EmailExists()
    {
        var repository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(x => x.ExistsByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("hash");

        var handler = new RegisterUserCommandHandler(repository.Object, passwordHasher.Object, unitOfWork.Object);

        var command = new RegisterUserCommand()
        {
            FirstName = "Anton",
            LastName = "Kuzmin",
            Email = "anton@gmail.com",
            Password = "password",
            ConfirmPassword = "password"
        };

        var act = () => handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync();
    }
}