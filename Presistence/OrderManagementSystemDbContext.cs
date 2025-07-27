using Domain.Models.CustomerModel;
using Domain.Models.InvoiceModel;
using Domain.Models.OrderModels;
using Domain.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
    {
    public class OrderManagementSystemDbContext : DbContext
        {
        public OrderManagementSystemDbContext(DbContextOptions<OrderManagementSystemDbContext> options)
            : base(options)
            {
            }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAssemblyReference).Assembly);

            base.OnModelCreating(modelBuilder);
            }

        }
    }
