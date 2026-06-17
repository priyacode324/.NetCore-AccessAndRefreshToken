using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myntra.DAL.Data;
using Myntra.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Configuration
{
    public static class DALExtension
    {
        public static IServiceCollection RegisterDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyntraDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddDbContext<MyntraDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
  
    }
}
