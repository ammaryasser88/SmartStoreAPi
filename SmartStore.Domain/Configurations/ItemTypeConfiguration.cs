using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities;

namespace SmartStore.Domain.Configurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("ItemTypes");
            builder.HasKey(g => g.ItemTypeId);
            builder.Property(g => g.NameArabic).IsRequired().HasMaxLength(100);
            builder.Property(g => g.NameEnglish).IsRequired().HasMaxLength(100);
        }
    }
}
