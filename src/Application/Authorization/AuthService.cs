using School.Application.Authorization.Requests;
using School.Application.Authorization.Responses;
using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Authorization;

public class AuthService(
    IUserRepository userRepository,
    IRolesRepository rolesRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
    )
{
    public async Task<RegisterUserResponse> Register(RegisterUserRequest command, CancellationToken cancellationToken)
    {
        if (command.Password != command.ConfirmPassword)
            throw new ArgumentException("Passwords do not match.");

        var email = Email.Create(command.Email);

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
            throw new ArgumentException("Email is already in use.");

        var fullName = FullName.Create(command.FirstName, command.LastName);
        var hashedPassword = passwordHasher.Hash(command.Password);
        var role = await rolesRepository.GetDefaultAsync(cancellationToken);

        var user = User.Register(
            fullName,
            email,
            hashedPassword,
            role.Id
        );

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse(user.Id);
    }

    public async Task<LoginUserResponse> Login(LoginUserRequest command, IJwtTokenGenerator jwtTokenGenerator, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);
        var user = await userRepository.GetByEmail(email, cancellationToken);

        if (user == null)
            return new LoginUserResponse(null, "Пользователь по данному email не зарегистрирован.");

        if (passwordHasher.Verify(command.Password, user.PasswordHash) == false)
            return new LoginUserResponse(null, "Неверный пароль.");

        var role = await rolesRepository.GetByIdAsync(user.RoleId, cancellationToken);

        var token = jwtTokenGenerator.Generate(new UserJwtPayload
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Role = role?.Name ?? "Unknown"
        });

        return new LoginUserResponse(token, null);
    }
}
