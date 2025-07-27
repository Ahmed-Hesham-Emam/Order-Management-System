
using AutoMapper;
using Order.Management.System.API.Extentions;

namespace Order.Management.System.API
    {
    public class Program
        {
        public static async Task Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();

            await app.ConfigureMiddlewares();

            app.Run();
            }
        }
    }
