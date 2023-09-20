using Remita.Models.Entities.Domians.User;

namespace Remita.Services.Domains.User;

public static class ApplicationUserExtensions
{
    public static string GetFullName(this ApplicationUser user)
    {
        string name = string.Empty;

        if (!string.IsNullOrWhiteSpace(user.FirstName))
        {
            name += $"{user.FirstName} ";
        }

        if (!string.IsNullOrWhiteSpace(user.MiddleName))
        {
            name += $"{user.MiddleName} ";
        }

        if (!string.IsNullOrWhiteSpace(user.LastName))
        {
            name += $"{user.LastName} ";
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            return name;
        }

        if (!string.IsNullOrWhiteSpace(user.UserName))
        {
            name = user.UserName;
        }
        return name;
    }
}
