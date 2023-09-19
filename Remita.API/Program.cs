using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Remita.Api.Infrastructure;
using Remita.Cache.Configuration;
using Remita.Cache.Extensions;
using Remita.Extensions;
using Remita.Services.Utility;
using System.Reflection;

namespace Remita.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration Configuration = builder.Configuration;
            IServiceCollection services = builder.Services;

            Settings setting = Configuration.Get<Settings>()!;
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }
            JwtConfig? jwtConfig = setting.JwtConfig;
            RedisConfig? redisConfig = setting.RedisConfig;
            if (jwtConfig == null)
            {
                throw new ArgumentNullException(nameof(jwtConfig));
            }

            if (redisConfig == null)
            {
                throw new ArgumentNullException(nameof(redisConfig));
            }

            services.AddSingleton(setting);
            services.AddSingleton(jwtConfig);
            services.AddSingleton(redisConfig);

            string? connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.SetupAppServices();
            services.RegisterDbContext(connectionString!);
            // services.RegisterAuthentication(jwtConfig);

            services.AddAutoMapper(Assembly.Load("Remita.Services"));

            services.AddRedisCache(redisConfig);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning(setup =>
             {
                 setup.DefaultApiVersion = new ApiVersion(1, 0);
                 setup.AssumeDefaultVersionWhenUnspecified = true;
                 setup.ReportApiVersions = true;
             })
       .AddVersionedApiExplorer(setup =>
       {
           setup.GroupNameFormat = "'v'VVV";
           setup.SubstituteApiVersionInUrl = true;
       });

            var app = builder.Build();
            IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseForwardedHeaders(new()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor
            });

            //  app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}