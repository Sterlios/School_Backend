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
    public async Task<RegisterUserResponse> Register(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        if (registerUserRequest.Password != registerUserRequest.ConfirmPassword)
            throw new ArgumentException("Passwords do not match.");

        var email = Email.Create(registerUserRequest.Email);

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
            throw new ArgumentException("Email is already in use.");

        var fullName = FullName.Create(registerUserRequest.FirstName, registerUserRequest.LastName);
        var hashedPassword = passwordHasher.Hash(registerUserRequest.Password);
        var role = await rolesRepository.GetDefaultAsync(cancellationToken);

        var user = User.Register(
            fullName,
            email,
            hashedPassword,
            role.Id
        );

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse(user.Id);
    }

    public async Task<LoginUserResponse> Login(LoginUserRequest loginUserRequest, IJwtTokenGenerator jwtTokenGenerator, CancellationToken cancellationToken)
    {
        var email = Email.Create(loginUserRequest.Email);
        var user = await userRepository.GetByEmail(email, cancellationToken);

        if (user == null)
            return new LoginUserResponse(null, "Пользователь по данному email не зарегистрирован.");

        if (passwordHasher.Verify(loginUserRequest.Password, user.PasswordHash) == false)
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
