using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Myntra.BLL.AccountServices;
using Myntra.BLL.RefreshServices;
using Myntra.DAL.Configuration;
using Myntra.DAL.Data;
using System.Text;

namespace Myntra.BLL.Configuration
{
    public static class BLLExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();

            var key = Encoding.ASCII.GetBytes(jwtOptions!.SecretKey);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });    
            services.RegisterDataContext(connectionString);
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRefreshService, RefreshService>();
            services.AddScoped<DatabaseSeederService>();
            return services;
        }
        public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var seedService = services.GetRequiredService<DatabaseSeederService>();
            var logger = services.GetRequiredService<ILogger<DatabaseSeederService>>();  

            try
            {
                await seedService.SeedAdminUserAsync();
                logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Seeding Error: {ex.Message}");   // Fallback
                throw;
            }
        }

    }
}
