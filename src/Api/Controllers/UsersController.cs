using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Authorization;
using School.Application.Users.BlockUser;
using School.Application.Users.GetUser;
using School.Application.Users.GetUsersList;
using School.Application.Users.LoginUser;
using School.Application.Users.RegisterUser;
using School.Application.Users.UnblockUser;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    RegisterUserCommandHandler registerUserCommandHandler,
    GetUserQueryHandler getUserHandler,
    LoginUserCommandHandler loginUserCommandHandler,
    GetUsersListHandler getUsersListHandler,
    BlockUserHandler blockUserHandler,
    UnblockUserHandler unblockUserHandler): ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        var user = await registerUserCommandHandler.Handle(command, ct);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId.Value }, user);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Application.Users.GetUser.GetUserResponse?>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await getUserHandler.Handle(new GetUserQuery(id), ct);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpPost]
    [Authorize(nameof(Permissions.ViewUsers))]
    public async Task<ActionResult<List<Application.Users.GetUsersList.GetUserResponse>>> GetUsers([FromQuery] FilterUsersListQuery query, CancellationToken ct)
    {
        var users = await getUsersListHandler.Handle(query, ct);

        return users;
    }

    [HttpPut("{id}/block")]
    [Authorize(nameof(Permissions.BlockUsers))]
    public async Task<IActionResult> BlockUser(Guid id, CancellationToken ct)
    {
        await blockUserHandler.Handle(new BlockUserQuery(id), ct);

        return NoContent();
    }

    [HttpPut("{id}/unblock")]
    [Authorize(nameof(Permissions.BlockUsers))]
    public async Task<IActionResult> UnblockUser(Guid id, CancellationToken ct)
    {
        await unblockUserHandler.Handle(new UnblockUserQuery(id), ct);

        return NoContent();
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
}
