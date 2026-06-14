namespace School.Domain.Models.Users;

public class User
{
    private readonly int _id;
    private readonly FullName _name;
    private readonly Email _email;
    private readonly Password _password;
    private readonly GlobalRoles _role;
    private readonly UserStatuses _status;
}
