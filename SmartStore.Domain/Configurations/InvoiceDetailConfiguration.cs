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
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetails");
            builder.HasKey(id => id.InvoiceDetailId);
            builder.Property(id => id.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(id => id.UnitPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(id => id.Item)
                   .WithMany(i => i.InvoiceDetails)
                   .HasForeignKey(id => id.ItemId);

            builder.HasOne(id => id.Invoice)
                   .WithMany(i => i.InvoiceDetails)
                   .HasForeignKey(id => id.InvoiceId);
        }
    }
}
