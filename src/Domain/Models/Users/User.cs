namespace School.Domain.Models.Users;

public class User
{
    private readonly int _id;
    private readonly FullName _name;
    private readonly Email _email;
    private readonly string _passwordHash;
    private readonly GlobalRoles _role;
    private readonly UserStatuses _status;

    private User(FullName name, Email email, string passwordHash)
    {
        _name = name;
        _email = email;
        _passwordHash = passwordHash;
        _role = GlobalRoles.User;
        _status = UserStatuses.Active;
    }

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
