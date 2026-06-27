using School.Domain.Users;

namespace School.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}