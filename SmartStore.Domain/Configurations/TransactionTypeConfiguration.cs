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
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("TransactionTypes");
            builder.HasKey(t => t.TypeId);
            builder.Property(t => t.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(t => t.NameEnglish).IsRequired().HasMaxLength(100);
        }
    }
}
