namespace School.Domain.Models.Users;

public class Password
{
    private readonly string _hash;

    private Password(string hash)
        => _hash = hash;
}