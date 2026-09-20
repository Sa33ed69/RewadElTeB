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
    public class HomeAppointmentConfiguration : IEntityTypeConfiguration<HomeAppointment>
    {
        public void Configure(EntityTypeBuilder<HomeAppointment> builder)
        {
            builder.ToTable("HomeAppointments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PatientName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PatientPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ServiceType)
                .IsRequired();

            builder.Property(x => x.CaseDescription)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
