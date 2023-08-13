using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NormandyHostelManager.Data.Interfaces;
using NormandyHostelManager.Models.DatabaseContexts;
using NormandyHostelManager.Models.Identity;
using NormandyHostelManager.Services.Mapper;
using NormandyHostelManager.Tests.Integration.Shared.Fixtures;

namespace NormandyHostelManager.Api.Tests.Integration.Helpers;
public abstract class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    protected readonly DatabaseFixtures _databaseFixture;
    protected readonly IMapper _mapper;
    protected readonly Dictionary<Type, object> _mockMap;

    private readonly CustomWebApplicationFactory<Program> _factory;

    public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
    {
        _databaseFixture = new DatabaseFixtures();
        _factory = factory;
        _mockMap = _factory.MockMap;
        _unitOfWork = GetService<IUnitOfWork<ApplicationDbContext>>();

        // We use extended mapper for tests, because of additional mappings that make writing tests easier
        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        }).CreateMapper();
    }


    protected HttpClient GetAuthorizedClient(Action<IServiceCollection> registratorCallback, string[] userRoles, string userId, string email, string userName)
    {
        return _factory.GetAuthorizedClient(registratorCallback, userRoles, userId, email, userName);
    }

    protected HttpClient GetAuthorizedClient(string[] userRoles, string userId, string email, string userName)
    {
        return _factory.GetAuthorizedClient(userRoles, userId, email, userName);
    }

    protected HttpClient GetClient()
    {
        return _factory.CreateClient();
    }

    protected T GetService<T>() where T : class
    {
        var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<T>();
        if (service == null)
        {
            throw new System.Exception("Failed to retrieve the service instance for type: " + typeof(T));
        }
        return (T)service;
    }

    protected async Task SetDefaultPasswords(string defaultPassword = "1111")
    {
        var users = await _unitOfWork.GetRepository<ApplicationUser>().GetQueryableList().ToListAsync();
        var userManager = GetService<UserManager<ApplicationUser>>();
        foreach (var user in users)
        {
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, defaultPassword);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    protected string FormatQueryString<T>(string url, T data) where T : class
    {
        Dictionary<string, string> query = data.GetType()
                                                    .GetProperties()
                                                    .ToDictionary(x => x.Name, x => x.GetValue(data)?.ToString() ?? "");
        var uri = QueryHelpers.AddQueryString(url, query);
        return uri;

    }

}
