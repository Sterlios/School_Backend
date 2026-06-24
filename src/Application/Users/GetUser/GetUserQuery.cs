namespace School.Application.Users.GetUser;

public class GetUserQuery
{
    public GetUserQuery(Guid id) =>
        Id = id;

    public Guid Id { get; private set; }
}