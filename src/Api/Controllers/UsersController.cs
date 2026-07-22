using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Users;
using School.Application.Users.Requests;
using School.Application.Users.Responses;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService userService): ControllerBase
{

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetUserResponse?>> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUser(new GetUserRequest(id), cancellationToken);

        if (user is null)
            return NotFound();

        return user;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetUserResponse>>> GetUsers([FromQuery] FilterUsersListRequest filterUsersListRequest, CancellationToken cancellationToken)
    {
        var users = await userService.GetUsersList(filterUsersListRequest, cancellationToken);

        return users;
    }

    [HttpPut("{id}/block")]
    [Authorize()]
    public async Task<IActionResult> BlockUser(Guid id, CancellationToken cancellationToken)
    {
        await userService.BlockUser(new BlockUserRequest(id), cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}/unblock")]
    [Authorize]
    public async Task<IActionResult> UnblockUser(Guid id, CancellationToken cancellationToken)
    {
        await userService.UnblockUser(new UnblockUserRequest(id), cancellationToken);

        return NoContent();
    }
}
