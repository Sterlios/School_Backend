using School.Application.Interfaces;
using School.Domain.Users;

namespace School.Application.Users.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);
        var user = await userRepository.GetByEmail(email);

        if (user == null)
            return new LoginUserResponse
            {
                ErrorStatus = "Пользователь по данному email не зарегистрирован."
            };

        if (passwordHasher.Verify(command.Password, user.PasswordHash) == false)
            return new LoginUserResponse
            {
                ErrorStatus = "Неверный пароль."
            };

        var token = jwtTokenGenerator.Generate(user);

        return new LoginUserResponse
        {
            UserId = user.Id,
        };
    }
}
