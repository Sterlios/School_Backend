using School.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace School.Domain.Users;

public class Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidEmailException(value, "Email cannot be empty or whitespace.");

        value = value.Trim().ToLowerInvariant();

        if (!IsValid(value))
            throw new InvalidEmailException(value, "Email format is invalid.");

        return new Email(value);
    }

    private static bool IsValid(string value)
    {
        return Regex.IsMatch(
            value,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool Equals(Email? other)
    {
        return other is not null &&
               Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Email);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
