namespace School.Domain.Exceptions;

public class InvalidEmailException: Exception
{
    public InvalidEmailException(string email, string? reason = null)
        : base(BuildMessage(email, reason))
    {
    }

    private static string BuildMessage(string email, string? reason)
    {
        return reason is null
            ? $"Invalid email: '{email}'."
            : $"Invalid email: '{email}'. Reason: {reason}";
    }
}
