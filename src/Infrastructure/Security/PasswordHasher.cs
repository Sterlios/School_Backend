using School.Application.Interfaces;
using System.Text;

namespace School.Infrastructure.Security;

public class PasswordHasher: IPasswordHasher
{
    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or whitespace.", nameof(password));
        // Use a simple hashing algorithm for demonstration purposes.
        // TODO: In a real application, use a secure hashing algorithm like BCrypt or Argon2.
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool Verify(string password, string hashedPassword)
    {
        var hashedInput = Hash(password);
        return hashedInput == hashedPassword;
    }
}
