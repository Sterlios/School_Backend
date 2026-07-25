namespace School.Application.Authorization.Requests;

public record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
