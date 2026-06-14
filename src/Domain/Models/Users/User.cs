namespace School.Domain.Models.Users;

public class User
{
    private User(FullName name, Email email, string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = GlobalRoles.User;
        Status = UserStatuses.Active;
    }

    public int Id { get; private set; }
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public GlobalRoles Role { get; private set; }
    public UserStatuses Status { get; private set; }

    public static User Register(FullName name, Email email, string passwordHash)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name), $"В {nameof(User)} поступил null вместо {nameof(name)}");

        if (email is null)
            throw new ArgumentNullException(nameof(email), $"В {nameof(User)} поступил null вместо {nameof(email)}");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException(nameof(passwordHash), $"В {nameof(User)} поступил пустой {nameof(passwordHash)}");

        return new User(name, email, passwordHash);
    }
}
