namespace School.Application.Users.Queries;

public class FilterUsersListQuery
{
    public int PageNumber { get; set; } = 1;
    public int TotalCount { get; set; }
}
