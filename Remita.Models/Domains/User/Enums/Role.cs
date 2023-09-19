namespace Remita.Models.Domains.User.Enums;
public enum UserRole
{
    Admin = 1,
    User,
    SuperAdmin,
}

public static class GetUserRole
{
    public static string? GetStringValue(this UserRole userRole)
    {
        return userRole switch
        {
            UserRole.Admin => "admin",
            UserRole.User => "user",
            UserRole.SuperAdmin => "superadmin",
            _ => null
        };
    }
}
