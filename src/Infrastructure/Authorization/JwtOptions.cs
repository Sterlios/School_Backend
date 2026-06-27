namespace School.Infrastructure.Authorization;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public int ExpirationMinutes { get; init; }
}
