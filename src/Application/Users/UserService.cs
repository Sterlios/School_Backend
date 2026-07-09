using School.Application.Authorization;
using School.Application.Interfaces;
using School.Application.Users.Queries;
using School.Application.Users.Responses;
using School.Domain.Users;

namespace School.Application.Users;

public class UserService(
    IUserRepository userRepository,
    IRolesRepository rolesRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IJwtTokenGenerator jwtTokenGenerator)
{
    public async Task<RegisterUserResponse> Register(RegisterUserCommand command, CancellationToken cancellationToken)
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

        return new RegisterUserResponse { UserId = user.Id };
    }

    public async Task<LoginUserResponse> Login(LoginUserCommand command, CancellationToken cancellationToken)
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

        var role = await rolesRepository.GetByIdAsync(user.RoleId);

        var token = jwtTokenGenerator.Generate(new UserPayload
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Role = role?.Name ?? "Unknown"
        });

        return new LoginUserResponse
        {
            Token = token,
        };
    }

    public async Task<List<GetUserResponse>> GetUsersList(FilterUsersListQuery filterUsersListQuery, CancellationToken ct)
    {
        var users = await userRepository.GetUsersAsync(filterUsersListQuery, ct);

        var roleIds = users
            .Select(u => u.RoleId)
            .Distinct()
            .ToList();

        var roles = await rolesRepository.GetAllByIdsAsync(roleIds);

        return users.Select(u => new GetUserResponse
        {
            Id = u.Id.Value,
            Name = string.Join(" ", u.Name.FirstName, u.Name.LastName),
            Email = u.Email.Value,
            Role = roles.FirstOrDefault(r => r.Id == u.RoleId)?.Name ?? "Unknown",
            Status = u.Status.ToString()
        }).ToList();
    }

    public async Task<GetUserResponse> GetUser(GetUserQuery getUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(getUserQuery.Id), ct);

        var role = await rolesRepository.GetByIdAsync(user.RoleId, ct);

        if (user is null)
        {
            throw new Exception($"User with id {getUserQuery.Id} not found.");
        }

        return new GetUserResponse
        {
            Id = user.Id.Value,
            Name = string.Join(" ", user.Name.FirstName, user.Name.LastName),
            Email = user.Email.Value,
            Role = role?.Name ?? "Unknown",
            Status = user.Status.ToString()
        };
    }

    public async Task BlockUser(BlockUserQuery blockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(blockUserQuery.Id), ct);

        if (user is null)
            throw new Exception($"User with id {blockUserQuery.Id} not found.");

        user.Block();
    }

    public async Task UnblockUser(UnblockUserQuery unblockUserQuery, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(new UserId(unblockUserQuery.Id), ct);

        if (user is null)
            throw new Exception($"User with id {unblockUserQuery.Id} not found.");

        user.Unblock();
    }
}
