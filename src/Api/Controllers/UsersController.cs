using Microsoft.AspNetCore.Mvc;
using School.Application.Users.GetUser;
using School.Application.Users.RegisterUser;

namespace School.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    RegisterUserCommandHandler registerUserCommandHandler,
    GetUserQueryHandler getUserHandler): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        var user = await registerUserCommandHandler.Handle(command, ct);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId.value }, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken ct)
    {
        var user = await getUserHandler.Handle(new GetUserQuery(id), ct);

        if (user is null)
            return NotFound();

        return Ok(user);
    }
}
