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
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(i => i.ItemId);
            builder.Property(i => i.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(i => i.NameEnglish).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Barcode).IsRequired().HasMaxLength(50);
            builder.Property(i => i.PurchasePrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.SalePrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Stock).HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Category)
                   .WithMany(c => c.Items)
                   .HasForeignKey(i => i.CategoryId);

            builder.HasOne(i => i.Type)
                   .WithMany(g => g.Items)
                   .HasForeignKey(i => i.TypeId);

            builder.HasOne(i => i.Unit)
                   .WithMany(u => u.Items)
                   .HasForeignKey(i => i.UnitId);
        }
    }
}
