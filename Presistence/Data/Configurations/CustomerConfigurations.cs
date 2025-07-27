using Domain.Models.CustomerModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
    {
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
        {
        public void Configure(EntityTypeBuilder<Customer> builder)
            {

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired();

            builder.HasMany(c => c.Orders)
                .WithOne()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            }
        }
    }
