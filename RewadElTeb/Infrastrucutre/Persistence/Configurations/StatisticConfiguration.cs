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
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {

            builder.ToTable("Statistics");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.UpdatedAt)
                .IsRequired();

            builder.HasIndex(s => s.Key)
                .IsUnique();
        }
    }
}
