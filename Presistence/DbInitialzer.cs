using Domain.Contracts;
using Domain.Models.IdentityModel;
using Domain.Models.ProductModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
    {
    public class DbInitialzer : IDbInitialzer
        {
        private readonly OrderManagementSystemIdentityDbContext _identityDbContext;
        private readonly OrderManagementSystemDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitialzer(OrderManagementSystemIdentityDbContext IdentityDbContext, OrderManagementSystemDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
            {
            _context = context;
            _identityDbContext = IdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            }

        public async Task InitialzeAsync()
            {

            if ( _context.Database.GetPendingMigrations().Any() )
                {
                await _context.Database.MigrateAsync();
                }


            if ( !_context.Products.Any() )
                {

                var productsData = await File.ReadAllTextAsync(@"..\Presistence\Data\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if ( products is not null && products.Any() )
                    {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                    }

                }

            }
        public async Task InitialzeIdentityAsync()
            {

            if ( _identityDbContext.Database.GetPendingMigrations().Any() )
                {
                await _identityDbContext.Database.MigrateAsync();
                }

            if ( !_roleManager.Roles.Any() )
                {
                await _roleManager.CreateAsync(new Role { Name = "Admin" });
                await _roleManager.CreateAsync(new Role { Name = "Customer" });
                }

            var existingAdmin = await _userManager.FindByNameAsync("Admin");
            if ( existingAdmin == null )
                {

                var adminUser = new User
                    {
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    };

                var result = await _userManager.CreateAsync(adminUser, "Pa$$w0rd");

                if ( result.Succeeded )
                    {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }


        }

    }


