using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Users.GetUser;
using School.Application.Users.LoginUser;
using School.Application.Users.RegisterUser;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    RegisterUserCommandHandler registerUserCommandHandler,
    GetUserQueryHandler getUserHandler,
    LoginUserCommandHandler loginUserCommandHandler): ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        var user = await registerUserCommandHandler.Handle(command, ct);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId.Value }, user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserResponse?>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await getUserHandler.Handle(new GetUserQuery(id), ct);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginUserResponse?>> Login([FromBody] LoginUserCommand loginUserCommand, CancellationToken ct)
    {
        var user = await loginUserCommandHandler.Handle(loginUserCommand, ct);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> GetMessage()
    {
        return Ok("Hello");
    }
}
