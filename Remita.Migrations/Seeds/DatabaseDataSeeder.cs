using Remita.Models.DatabaseContexts;
using Remita.Models.Domains.User.Enums;
using Remita.Models.Entities.Domians.User;

namespace Remita.Data.Seeds;

public static class DatabaseDataSeeder
{
    public static void SeedData(this ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
        var roleExist = context.UserRoles.Any();

        if (!roleExist)
        {
            context.Roles.AddRange(SeededRoles());
            context.SaveChanges();
        }

        var resultExists = context.Results.Any();

    }


    private static IEnumerable<ApplicationRole> SeededRoles()
    {
        return new List<ApplicationRole>()
        {
            new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = UserTypeExtension.GetStringValue(UserType.User),
                Type = UserType.User,
                NormalizedName = UserTypeExtension.GetStringValue(UserType.User)?.ToUpper()
                                                  .Normalize()
            },
            new ApplicationRole()
            {
                 Id = Guid.NewGuid().ToString(),
                Name = UserTypeExtension.GetStringValue(UserType.Admin),
                Type = UserType.Admin,
                NormalizedName = UserTypeExtension.GetStringValue(UserType.Admin)?.ToUpper()
                                                  .Normalize()
            },
            new ApplicationRole
            {
                 Id = Guid.NewGuid().ToString(),
                Name = UserTypeExtension.GetStringValue(UserType.SuperAdmin),
                Type = UserType.SuperAdmin,
                NormalizedName = UserTypeExtension.GetStringValue(UserType.SuperAdmin)?.ToUpper()
                                                  .Normalize()
            }
        };
    }

}
