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
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(
           EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.UserEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.EntityId)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
