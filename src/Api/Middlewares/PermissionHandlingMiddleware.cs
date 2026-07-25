using School.Application.Interfaces;
using System.Security.Claims;

namespace School.Api.Middlewares;

public class PermissionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRolesRepository _rolesRepository;

    public PermissionHandlingMiddleware(RequestDelegate next, IRolesRepository rolesRepository)
    {
        _next = next;
        _rolesRepository = rolesRepository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var permissions = await _rolesRepository.GetPermissionsForRole(userRole);

        context.User.AddIdentity(new ClaimsIdentity(permissions.Select(p => new Claim("Permission", p.Name))));

        await _next(context);
    }
}