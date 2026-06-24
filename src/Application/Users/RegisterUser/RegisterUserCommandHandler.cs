using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Users.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
)
{
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Password != command.ConfirmPassword)
            throw new ArgumentException("Passwords do not match.");

        var email = Email.Create(command.Email);

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
            throw new ArgumentException("Email is already in use.");

        var fullName = FullName.Create(command.FirstName, command.LastName);
        var hashedPassword = passwordHasher.Hash(command.Password);

        var user = User.Register(
            fullName,
            email,
            hashedPassword
        );

        await userRepository.AddAsync(user, cancellationToken);

        return new RegisterUserResponse { UserId = user.Id };
    }
}
