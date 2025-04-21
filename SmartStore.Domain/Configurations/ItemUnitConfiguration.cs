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
    public class UnitConfiguration : IEntityTypeConfiguration<ItemUnit>
    {
        public void Configure(EntityTypeBuilder<ItemUnit> builder)
        {
            builder.ToTable("ItemUnits");
            builder.HasKey(u => u.ItemUnitId);
            builder.Property(u => u.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(u => u.NameEnglish).IsRequired().HasMaxLength(100);
        }
    }
}
