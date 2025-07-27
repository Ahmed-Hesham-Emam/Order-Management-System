using Domain.Models.InvoiceModel;
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
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
        {
        public void Configure(EntityTypeBuilder<Invoice> builder)
            {

            builder.HasOne(i => i.Order)
                .WithOne(o => o.Invoice)
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(i => i.TotalAmount)
                .HasPrecision(18, 2);
            }
        }
    }
