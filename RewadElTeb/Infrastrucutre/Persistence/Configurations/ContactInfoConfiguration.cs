using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email).HasMaxLength(150);
            builder.Property(c => c.Phone).HasMaxLength(20);
            builder.Property(c => c.WhatsApp).HasMaxLength(20);
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.Hours).HasMaxLength(200);
            builder.Property(c => c.MapUrl).HasMaxLength(1000);

            builder.Property(c => c.UpdatedAt)
                .IsRequired();
        }
    }
}
