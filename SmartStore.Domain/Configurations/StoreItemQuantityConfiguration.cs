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
    public class StoreItemQuantityConfiguration : IEntityTypeConfiguration<StoreItemQuantity>
    {
        public void Configure(EntityTypeBuilder<StoreItemQuantity> builder)
        {
            builder.ToTable("StoreItemQuantities");
            builder.HasKey(sq => new { sq.StoreId, sq.ItemId });
            builder.Property(sq => sq.Quantity).HasColumnType("decimal(18,2)");

            builder.HasOne(sq => sq.Store)
                   .WithMany(s => s.StoreItemQuantities)
                   .HasForeignKey(sq => sq.StoreId);

            builder.HasOne(sq => sq.Item)
                   .WithMany(i => i.StoreItemQuantities)
                   .HasForeignKey(sq => sq.ItemId);
        }
    }
}
