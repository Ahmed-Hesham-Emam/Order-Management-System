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
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
        {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
            {



            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
                .WithOne(oi => oi.orderItem)
                .HasForeignKey<OrderItem>(oi => oi.OrderId);

            builder.Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            }
        }
    }
