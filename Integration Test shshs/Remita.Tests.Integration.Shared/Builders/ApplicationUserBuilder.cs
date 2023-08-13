using Bogus;
using NormandyHostelManager.Models.Identity;

namespace NormandyHostelManager.Tests.Integration.Shared.Builders;
public class ApplicationUserBuilder : BuilderBase<ApplicationUser>
{
    public ApplicationUserBuilder()
    {
    }

    public override void ConfigureRule(Faker<ApplicationUser> faker)
    {
        faker
           .RuleFor(d => d.UserName, f => f.Name.FullName())
           .RuleFor(d => d.Email, f => f.Internet.Email())
           .RuleFor(d => d.EmailConfirmed, f => true)
           .RuleFor(d => d.PhoneNumber, f => f.Random.ReplaceNumbers("0###########"))
           .RuleFor(d => d.Id, f => f.Random.Guid().ToString())
           .RuleFor(d => d.CreatedAt, f => f.Date.Recent(100))
           .FinishWith((_, user) =>
           {
               user.NormalizedEmail = user.Email?.ToUpperInvariant();
               user.NormalizedUserName = user.UserName?.ToUpperInvariant();
           });
    }
}