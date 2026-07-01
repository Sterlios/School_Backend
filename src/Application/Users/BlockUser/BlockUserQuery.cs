namespace School.Application.Users.BlockUser;

public class BlockUserQuery
{
    public BlockUserQuery(Guid id) =>
        Id = id;

    public Guid Id { get; private set; }
}
