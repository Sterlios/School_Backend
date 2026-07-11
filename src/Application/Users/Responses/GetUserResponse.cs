namespace School.Application.Users.Responses;

public record GetUserResponse(Guid Id, string Name, string Email, string Role, string Status);
