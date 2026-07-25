namespace School.Domain.Users.Roles;

/// <summary>
/// Роль пользователя в системе, определяющая его права доступа и разрешения к целой платформе.
/// </summary>
public class GlobalRole
{
    public GlobalRoleId Id { get; init; }
    public string Name { get; init; }
    public bool IsDefault { get; init; } = false;

    public List<Permission> Permissions { get; } = new List<Permission>();
}