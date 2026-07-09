namespace School.Domain.Users;

public class User //TODO: Create Custom Exceptions for User domain
{
    private User(FullName name, Email email, string passwordHash, int roleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        RoleId = roleId;
        Status = UserStatuses.Active;
    }

    private User() { } // For EF

    public UserId Id { get; }
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public int RoleId { get; private set; }
    public UserStatuses Status { get; private set; }

    public static User Register(FullName name, Email email, string passwordHash, int roleId)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        return new User(name, email, passwordHash, roleId);
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

    public void ChangeRole(int roleId)
    {
        if (RoleId == roleId)
            throw new InvalidOperationException($"Не удалось изменить роль у пользователя.");

        RoleId = roleId;
    }

    public void ChangePassword(string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        if (PasswordHash == passwordHash)
            throw new InvalidOperationException($"Новый пароль не должен совпадать с текущим.");

        PasswordHash = passwordHash;
    }
}
