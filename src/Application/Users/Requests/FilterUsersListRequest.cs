namespace School.Application.Users.Requests;

public record FilterUsersListRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
