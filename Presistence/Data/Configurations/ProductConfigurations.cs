using Domain.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
    {
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
        {
        public void Configure(EntityTypeBuilder<Product> builder)
            {

            builder.Property(p => p.Price)
                .HasPrecision(18, 2);
            }
        }
    }
