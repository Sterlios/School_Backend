using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Authorization;
using School.Application.Authorization.Requests;
using School.Application.Authorization.Responses;
using School.Application.Interfaces;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService): ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest command, CancellationToken ct)
    {
        var user = await authService.Register(command, ct);
        return CreatedAtAction("GetUser", new { id = user.UserId.Value }, user);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginUserResponse?>> Login([FromBody] LoginUserRequest loginUserCommand, [FromServices] IJwtTokenGenerator jwtTokenGenerator, CancellationToken ct)
    {
        var user = await authService.Login(loginUserCommand, jwtTokenGenerator, ct);

        if (user is null)
            return NotFound();

        return user;
    }
}
