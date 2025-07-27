using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Identity
    {
    public class OrderManagementSystemIdentityDbContext(DbContextOptions<OrderManagementSystemIdentityDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
        {
        protected override void OnModelCreating(ModelBuilder builder)
            {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<User>().HasIndex(u => u.NormalizedUserName).IsUnique();
            builder.Entity<User>().Property(u => u.Email).IsRequired(false);
            builder.Entity<User>().Property(u => u.NormalizedEmail).IsRequired(false);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

            builder.Entity<IdentityUserRole<Guid>>()
           .HasOne<User>()
           .WithMany()
           .HasForeignKey(ur => ur.UserId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityUserRole<Guid>>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
