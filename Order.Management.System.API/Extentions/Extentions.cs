using Domain.Contracts;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Order.Management.System.API.Middlewares;
using Presistence;
using Presistence.Identity;
using Services;
using Shared;
using System.Text;

namespace Order.Management.System.API.Extentions
    {
    public static class Extentions
        {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
            {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices(configuration);
            services.AddIdentityServices();

            services.ConfigureJwt(configuration);
            return services;
            }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
            {
            services.AddControllers();
            return services;
            }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
            {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
            }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
            {
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<OrderManagementSystemIdentityDbContext>();
            return services;
            }

        private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
            {
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                    {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    };
            });

            return services;

            }


        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
            {

            await app.InitiateDataBaseAsync();
            app.UseGlobalErrorHandling();

            if ( app.Environment.IsDevelopment() )
                {
                app.UseSwagger();
                app.UseSwaggerUI();
                }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            return app;
            }


        private static async Task<WebApplication> InitiateDataBaseAsync(this WebApplication app)
            {

            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbInit = scope.ServiceProvider.GetRequiredService<IDbInitialzer>();
            await dbInit.InitialzeAsync();
            await dbInit.InitialzeIdentityAsync();
            #endregion

            return app;
            }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
            {

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
            }
        }

    }
