namespace School.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RequiredPermissionsAttribute: Attribute
{
    private readonly string _permission;

    public RequiredPermissionsAttribute(string permission) =>
        _permission = permission;


}
