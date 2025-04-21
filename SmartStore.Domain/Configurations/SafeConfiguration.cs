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
    public class SafeConfiguration : IEntityTypeConfiguration<Safe>
    {
        public void Configure(EntityTypeBuilder<Safe> builder)
        {
            builder.ToTable("Safes");
            builder.HasKey(s => s.SafeId);
            builder.Property(s => s.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(s => s.NameEnglish).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Balance).HasColumnType("decimal(18,2)");
        }
    }
}
