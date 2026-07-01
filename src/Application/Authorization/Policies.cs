using School.Domain.Users;

namespace School.Application.Authorization;

public class Policies
{
    public static string Admin = GlobalRoles.Admin.ToString();
    public static string[] AdminPermissions = {
        Permissions.ViewUsers.ToString(),
        Permissions.BlockUsers.ToString(),
        Permissions.ChangeUserRoles.ToString(),
    };

    public static string User = GlobalRoles.User.ToString();
    public static string[] UserPermissions = {
        Permissions.ViewUsers.ToString(),
    };
}
