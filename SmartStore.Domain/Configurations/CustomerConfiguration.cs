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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.CustomerId);
            builder.Property(c => c.NameArabic)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.NameEnglish)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
