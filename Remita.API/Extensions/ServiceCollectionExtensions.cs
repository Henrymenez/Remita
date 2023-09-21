using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Remita.Data.Implementation;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Admin;
using Remita.Services.Domains.Auth;
using Remita.Services.Domains.OutboundNotifications;
using Remita.Services.Domains.Roles;
using Remita.Services.Domains.Security;
using Remita.Services.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Remita.Extensions;
public static class ServiceCollectionExtensions
{

    public static void SetupAppServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<ApplicationDbContext>, UnitOfWork<ApplicationDbContext>>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAccountLockoutService, AccountLockoutService>();
        services.AddTransient<INotificationManagerService, NotificationManagerService>();
        services.AddTransient<IOtpCodeService, OtpCodeService>();
        services.AddTransient<IAdminService,AdminService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IRoleClaimService, RoleClaimService>();



    }

    public static void RegisterDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(connectionString, s =>
            {
                s.MigrationsAssembly("Remita.Migrations");
                s.EnableRetryOnFailure(3);
            });
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // resolve the IApiVersionDescriptionProvider service
            // note: that we have to build a temporary service provider here because one has not been created yet
            var provider = services.BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            // integrate XML comments
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            options.EnableAnnotations();
        });
    }


    public static void RegisterAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey));
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = serverSecret,
                ValidIssuer = jwtConfig.JwtIssuer,
                ValidAudience = jwtConfig.JwtAudience,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };
        });

        services.AddAuthorization();
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = $"MakeWeBet API v{description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = "The API application for the MakeWeBet mobile application.",
            Contact = new OpenApiContact() { Name = "MakeWeBet Dev Support", Email = "help@MakeWeBet.com" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
