using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace NormandyHostelManager.Tests.Integration.Shared.Helpers;
public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string[] Roles { get; set; } = new string[] { "Admin" };
    public string UserName { get; set; } = "Test User";

    public string Email { get; set; } = "test@normandyhostelmanager.com";
    public string UserId { get; set; } = "4f8b3f4b-ffa0-4da5-9cd2-cba94185f390";

    public TestAuthenticationSchemeOptions(string[] roles, string name, string userId) : base()
    {
        Roles = roles;
        UserName = name;
        UserId = userId;
    }

    public TestAuthenticationSchemeOptions() : base()
    { }
}

public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        IdentityOptions _options = new IdentityOptions();

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, Options.Email),
                new Claim(JwtRegisteredClaimNames.Sub, Options.UserName),
                new Claim(ClaimTypes.Name, Options.UserName),
                new Claim(ClaimTypes.NameIdentifier, Options.UserId),
                new Claim(_options.ClaimsIdentity.UserIdClaimType,Options.UserId),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, Options.UserName),
            };

        foreach (var role in Options.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}