namespace School.Domain.Users.Roles;

public readonly struct GlobalRoleId
{
    public GlobalRoleId(int id) =>
        Id = id;

    public int Id { get; }
}
