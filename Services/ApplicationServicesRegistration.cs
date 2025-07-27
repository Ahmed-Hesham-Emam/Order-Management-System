using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public static class ApplicationServicesRegistration
        {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
            {
            //services.AddAutoMapper(typeof(ServiceAssemplyReference).Assembly);
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ServiceAssemplyReference).Assembly));
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtBearerOptions>(configuration.GetSection("JwtOptions"));
            return services;
            }
        }
    }
