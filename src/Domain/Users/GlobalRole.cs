using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Users;

/// <summary>
/// Роль пользователя в системе, определяющая его права доступа и разрешения к целой платформе.
/// </summary>
public class GlobalRole
{
    public int Id { get; }
    public string Name { get; }
    public bool IsDefault { get; } = false;

    [NotMapped]
    public List<Permission> Permissions { get; } = new List<Permission>();
}