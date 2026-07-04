using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Users;
using School.Application.Users.Queries;
using School.Application.Users.Responses;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService userService): ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        var user = await userService.Register(command, ct);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId.Value }, user);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetUserResponse?>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await userService.GetUser(new GetUserQuery(id), ct);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<List<GetUserResponse>>> GetUsers([FromQuery] FilterUsersListQuery query, CancellationToken ct)
    {
        var users = await userService.GetUsersList(query, ct);

        return users;
    }

    [HttpPut("{id}/block")]
    [Authorize]
    public async Task<IActionResult> BlockUser(Guid id, CancellationToken ct)
    {
        await userService.BlockUser(new BlockUserQuery(id), ct);

        return NoContent();
    }

    [HttpPut("{id}/unblock")]
    [Authorize]
    public async Task<IActionResult> UnblockUser(Guid id, CancellationToken ct)
    {
        await userService.UnblockUser(new UnblockUserQuery(id), ct);

        return NoContent();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginUserResponse?>> Login([FromBody] LoginUserCommand loginUserCommand, CancellationToken ct)
    {
        var user = await userService.Login(loginUserCommand, ct);

        if (user is null)
            return NotFound();

        return user;
    }
}
