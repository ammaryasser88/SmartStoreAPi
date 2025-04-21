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
    public class RevenueConfiguration : IEntityTypeConfiguration<Revenue>
    {
        public void Configure(EntityTypeBuilder<Revenue> builder)
        {
            builder.ToTable("Revenues");
            builder.HasKey(r => r.RevenueId);
            builder.Property(r => r.Amount).HasColumnType("decimal(18,2)");
            builder.Property(r => r.Date).HasColumnType("datetime");
            builder.Property(r => r.Notes).HasMaxLength(500);

            builder.HasOne(r => r.TransactionType)
                   .WithMany(t => t.Revenues)
                   .HasForeignKey(r => r.TypeId);
        }
    }
}
