namespace School.Domain.Users;

public class User //TODO: Create Custom Exceptions for User domain
{
    private User(FullName name, Email email, string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = GlobalRoles.User;
        Status = UserStatuses.Active;
    }

    public UserId Id { get; }
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public GlobalRoles Role { get; private set; }
    public UserStatuses Status { get; private set; }

    public static User Register(FullName name, Email email, string passwordHash)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        return new User(name, email, passwordHash);
    }

    public void Block()
    {
        if (Status == UserStatuses.Blocked)
            throw new InvalidOperationException($"Пользователь уже заблокирован.");

        Status = UserStatuses.Blocked;
    }

    public void Unblock()
    {
        if (Status == UserStatuses.Active)
            throw new InvalidOperationException($"Пользователь уже активен.");

        Status = UserStatuses.Active;
    }

    public void ChangeRole(GlobalRoles newRole)
    {
        if (Role == newRole)
            throw new InvalidOperationException($"Не удалось изменить роль у пользователя.");

        Role = newRole;
    }

    public void ChangePassword(string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        if (PasswordHash == passwordHash)
            throw new InvalidOperationException($"Новый пароль не должен совпадать с текущим.");

        PasswordHash = passwordHash;
    }
}
