namespace School.Application.Authorization.Responses;

public record LoginUserResponse(string? Token, string? ErrorStatus);
