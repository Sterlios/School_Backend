using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Users;
using School.Application.Users.Queries;
using School.Application.Users.Requests;
using School.Application.Users.Responses;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService userService): ControllerBase
{

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetUserResponse?>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await userService.GetUser(new GetUserRequest(id), ct);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetUserResponse>>> GetUsers([FromQuery] FilterUsersListRequest query, CancellationToken ct)
    {
        var users = await userService.GetUsersList(query, ct);

        return users;
    }

    [HttpPut("{id}/block")]
    [Authorize]
    public async Task<IActionResult> BlockUser(Guid id, CancellationToken ct)
    {
        await userService.BlockUser(new BlockUserRequest(id), ct);

        return NoContent();
    }

    [HttpPut("{id}/unblock")]
    [Authorize]
    public async Task<IActionResult> UnblockUser(Guid id, CancellationToken ct)
    {
        await userService.UnblockUser(new UnblockUserRequest(id), ct);

        return NoContent();
    }
}
