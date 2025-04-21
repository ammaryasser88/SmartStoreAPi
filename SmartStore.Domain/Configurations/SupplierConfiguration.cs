using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(s => s.SupplierId);
            builder.Property(s => s.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameEnglish).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Phone).IsRequired().HasMaxLength(15);
            builder.Property(s => s.Address).HasMaxLength(250);
        }
    }
}
