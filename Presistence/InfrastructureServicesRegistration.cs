using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Identity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
    {
    public static class InfrastructureServicesRegistration
        {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
            {

            services.AddDbContext<OrderManagementSystemDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddDbContext<OrderManagementSystemIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDbInitialzer, DbInitialzer>();


            return services;
            }
        }
    }
