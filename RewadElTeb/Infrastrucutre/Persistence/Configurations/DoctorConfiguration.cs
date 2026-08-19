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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(d => d.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(d => d.Biography)
                .HasMaxLength(2000);

            builder.Property(d => d.ImageUrl)
                .HasMaxLength(500);

            builder.Property(d => d.Status)
                .IsRequired()
                .HasConversion<string>()   // يخزن الـ Enum كـ نص مقروء في الداتابيز بدل رقم
                .HasMaxLength(20);

            builder.HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
