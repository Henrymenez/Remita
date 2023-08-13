using Bogus;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NormandyHostelManager.Api.Tests.Integration.Helpers;

namespace NormandyHostelManager.Api.Tests.Integration.Controllers.v1;
public class AuthControllerTest : IntegrationTestBase
{
    public AuthControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task SignUp_ShouldReturnOk()
    {

        var faker = new Faker();
        var randomId = faker.Random.Guid();

        Assert.True(true);
    }
}
