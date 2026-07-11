using School.Application.Authorization;

namespace School.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(UserJwtPayload user);
}