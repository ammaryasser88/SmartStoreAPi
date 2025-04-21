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
    public class SafeTransactionConfiguration : IEntityTypeConfiguration<SafeTransaction>
    {
        public void Configure(EntityTypeBuilder<SafeTransaction> builder)
        {
            builder.ToTable("SafeTransactions");
            builder.HasKey(st => st.TransactionId);
            builder.Property(st => st.Amount).HasColumnType("decimal(18,2)");
            builder.Property(st => st.Date).HasColumnType("datetime");
            builder.Property(st => st.Description).HasMaxLength(500);

            builder.HasOne(st => st.Safe)
                   .WithMany(s => s.SafeTransactions)
                   .HasForeignKey(st => st.SafeId);
        }
    }
}
