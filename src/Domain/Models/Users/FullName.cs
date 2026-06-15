namespace School.Domain.Models.Users;

public class FullName
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName));

        return new FullName(firstName, lastName);
    }
}