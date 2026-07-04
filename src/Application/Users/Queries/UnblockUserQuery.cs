namespace School.Application.Users.Queries;

public class UnblockUserQuery
{
    public UnblockUserQuery(Guid id) =>
        Id = id;

    public Guid Id { get; private set; }
}
