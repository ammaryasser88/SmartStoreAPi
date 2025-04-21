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
    public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            builder.ToTable("ItemCategory");
            builder.HasKey(c => c.ItemCategoryId);
            builder.Property(c => c.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(c => c.NameEnglish).IsRequired().HasMaxLength(100);
        }
    }
}
