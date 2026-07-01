namespace School.Application.Users.GetUsersList;

public class FilterUsersListQuery
{
    public int PageNumber { get; set; } = 1;
    public int TotalCount { get; set; }
}
