using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Remita.Tests.Integration.Shared.Helpers;

namespace Remita.Api.Tests.Integration.Helpers;
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public Dictionary<Type, object> MockMap { get; set; }
    public CustomWebApplicationFactory() : base()
    {
        MockMap = new Dictionary<Type, object>();
        // InitializeMocks();
    }

    private void RemoveOriginalServices(IServiceCollection services)
    {
        foreach (var item in MockMap)
        {
            var serviceDescriptor = services.First(
                descriptor => descriptor.ServiceType == item.Key);
            services.Remove(serviceDescriptor);
        }
    }

    /* private void InitializeMocks()
     {
         var commandClientMock = new Mock<ICommandClient>(MockBehavior.Strict);
         MockMap[typeof(ICommandClient)] = commandClientMock;

     }*/


    /*protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            RemoveOriginalServices(services);

            services.AddSingleton<ICommandClient>(sp =>
                  ((Mock<ICommandClient>)MockMap[typeof(ICommandClient)]).Object
              );

        });
    }*/

    public HttpClient GetAuthorizedClient(
        string[] userRoles,
        string userId,
        string email,
        string userName)
    {
        return this.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");

            builder.UseSetting(WebHostDefaults.EnvironmentKey, "Test");
            builder.ConfigureTestServices(services =>
            {
                RemoveOriginalServices(services);
                /*  services.AddSingleton<ICommandClient>(sp =>
                        ((Mock<ICommandClient>)MockMap[typeof(ICommandClient)]).Object
                    );*/

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                }).AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(
                        "Test", options => { options.Roles = userRoles; options.Email = email; options.UserId = userId; options.UserName = userName; });
                services.Configure<TestServer>(options => options.AllowSynchronousIO = true);

            });
        })
            .CreateClient();
    }


    public HttpClient GetAuthorizedClient(Action<IServiceCollection> registratorCallback,
                                          string[] userRoles,
                                          string userId,
                                          string email,
                                          string userName)
    {
        return this.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");

            builder.UseSetting(WebHostDefaults.EnvironmentKey, "Test");
            builder.ConfigureTestServices(services =>
            {
                registratorCallback(services);

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                }).AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(
                        "Test", options => { options.Roles = userRoles; options.Email = email; options.UserId = userId; options.UserName = userName; });
                services.Configure<TestServer>(options => options.AllowSynchronousIO = true);

            });
        })
            .CreateClient();
    }


}