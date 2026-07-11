using Microsoft.AspNetCore.Mvc.Filters;
using School.Application.Authorization;
using School.Domain.Users.Roles;
using System.Security.Claims;

namespace School.Api.Controllers;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeRequireAttribute: Attribute, IAsyncAuthorizationFilter
{
    private readonly Permission _permission;
    private readonly Policies _policies = new Policies();

    public AuthorizeRequireAttribute(Permission permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var roleClaim = context.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Role);

        Console.WriteLine(context.HttpContext.User?.ToString());

        //if (roleClaim == null || !_policies.HasPermission((GlobalRole)Enum.Parse(typeof(GlobalRole), roleClaim.Value), _permission))
        //{
        //    context.Result = new ForbidResult();
        //}
    }
}