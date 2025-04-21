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
    public class StockAdjustmentConfiguration : IEntityTypeConfiguration<StockAdjustment>
    {
        public void Configure(EntityTypeBuilder<StockAdjustment> builder)
        {
            builder.ToTable("StockAdjustments");
            builder.HasKey(sa => sa.StockAdjustmentId);
            builder.Property(sa => sa.QuantityBefore).HasColumnType("decimal(18,2)");
            builder.Property(sa => sa.QuantityAfter).HasColumnType("decimal(18,2)");
            builder.Property(sa => sa.Date).HasColumnType("datetime");
            builder.Property(sa => sa.Reason).IsRequired().HasMaxLength(250);

            builder.HasOne(sa => sa.Store)
                   .WithMany(s => s.StockAdjustments)
                   .HasForeignKey(sa => sa.StoreId);

            builder.HasOne(sa => sa.Item)
                   .WithMany(i => i.StockAdjustments)
                   .HasForeignKey(sa => sa.ItemId);
        }
    }
}
