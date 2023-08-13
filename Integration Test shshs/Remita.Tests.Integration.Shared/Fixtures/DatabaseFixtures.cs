using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NormandyHostelManager.Models.DatabaseContexts;
using NormandyHostelManager.Models.Domains.User.Enums;
using NormandyHostelManager.Tests.Integration.Shared.Builders;
using NormandyHostelManager.Tests.Integration.Shared.Helpers;

namespace NormandyHostelManager.Tests.Integration.Shared.Fixtures;
public class DatabaseFixtures : IDisposable
{
    public ApplicationDbContext ApplicationDbContext { get; private set; }
    public IntegratedTestCache Cache { get; private set; }

    public DatabaseFixtures()
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                        .AddJsonFile($"appsettings.Test.json", optional: false)
                        .AddEnvironmentVariables();

        var config = builder.Build();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        string? connectionString = config.GetValue<string>("ConnectionString");
        optionsBuilder.UseSqlServer(connectionString, s =>
        {
            s.MigrationsAssembly("NormandyHostelManager.Migrations");
            s.EnableRetryOnFailure(3);
        });

        optionsBuilder.UseLazyLoadingProxies();

        ApplicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
        ApplicationDbContext.Database.EnsureDeleted();
        ApplicationDbContext.Database.Migrate();
        Cache = new IntegratedTestCache();
        InitializeTestDatabase();
    }

    public void InitializeTestDatabase()
    {
        var faker = new Faker();

        #region Seed Roles
        var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
        var roleRecords = new List<IdentityRole>();
        foreach (var role in roles)
        {

            var roleName = role.ToString();
            roleRecords.Add(new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString()
            });

        }
        ApplicationDbContext.Roles.AddRange(roleRecords);
        ApplicationDbContext.SaveChanges();
        Cache.AddRange(roleRecords);
        #endregion

        #region Seed Users
        var userBuilder = new ApplicationUserBuilder();
        var users = userBuilder.Generate(10).GetAll();

        ApplicationDbContext.Users.AddRange(users);
        ApplicationDbContext.SaveChanges();
        Cache.AddRange(users);
        #endregion

        #region Seed User Roles
        var userRole = Cache.Get<IdentityRole>().First(x => x.Name == UserRole.User.ToString());
        var userRoles = new List<IdentityUserRole<string>>();

        foreach (var user in users)
        {
            var identityUserRole = new IdentityUserRole<string>()
            {
                RoleId = userRole.Id,
                UserId = user.Id,
            };
            userRoles.Add(identityUserRole);
        }
        ApplicationDbContext.UserRoles.AddRange(userRoles);
        ApplicationDbContext.SaveChanges();
        #endregion
    }

    public void Dispose()
    {
        ApplicationDbContext.Database.EnsureDeleted();
    }
}