using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
    {
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
        {
        public void Configure(EntityTypeBuilder<Order> builder)
            {


            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);
            }
        }
    }
