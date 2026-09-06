using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DataSeeding
{
    public class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedDepartmentsAndDoctorsAsync(context);
            await SeedAppointmentsAsync(context);
            await SeedDevicesAsync(context);
            await SeedStaffAsync(context);
            await SeedContractsAsync(context);
            await SeedStatisticsAsync(context);
            await SeedContactInfoAsync(context);
        }

        private static async Task SeedDepartmentsAndDoctorsAsync(AppDbContext context)
        {
            if (await context.Departments.AnyAsync()) return;

            var departments = new List<Department>
            {
                new Department
                {
                    Name = "أمراض القلب",
                    Description = "تشخيص وعلاج أمراض القلب والشرايين بأحدث التقنيات",
                    ImageUrl = null
                },
                new Department
                {
                    Name = "الباطنة",
                    Description = "تشخيص وعلاج الأمراض الداخلية للبالغين",
                    ImageUrl = null
                },
                new Department
                {
                    Name = "العظام",
                    Description = "علاج إصابات وأمراض العظام والمفاصل والعمود الفقري",
                    ImageUrl = null
                },
                new Department
                {
                    Name = "الأنف والأذن والحنجرة",
                    Description = "تشخيص وعلاج أمراض الأنف والأذن والحنجرة",
                    ImageUrl = null
                }
            };

            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();

            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    FullName = "د. أحمد الشريف",
                    Specialization = "استشاري باطنة",
                    Biography = "خبرة أكثر من 15 عام في علاج الأمراض الباطنية",
                    Status = DoctorStatus.Active,
                    DepartmentId = departments[1].Id   // الباطنة
                },
                new Doctor
                {
                    FullName = "د. خالد المطيري",
                    Specialization = "استشاري عظام",
                    Biography = "متخصص في جراحات المفاصل الدقيقة",
                    Status = DoctorStatus.Active,
                    DepartmentId = departments[2].Id   // العظام
                },
                new Doctor
                {
                    FullName = "د. منى الحربي",
                    Specialization = "استشاري عظام أطفال",
                    Biography = "خبرة واسعة في علاج تشوهات العظام لدى الأطفال",
                    Status = DoctorStatus.Active,
                    DepartmentId = departments[2].Id   // العظام
                },
                new Doctor
                {
                    FullName = "د. يوسف البقمي",
                    Specialization = "استشاري أنف وأذن وحنجرة",
                    Biography = "متخصص في جراحات الأذن الدقيقة",
                    Status = DoctorStatus.Active,
                    DepartmentId = departments[3].Id   // أنف وأذن وحنجرة
                }
            };

            context.Doctors.AddRange(doctors);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAppointmentsAsync(AppDbContext context)
        {
            if (await context.Appointments.AnyAsync()) return;

            var doctors = await context.Doctors.ToListAsync();
            if (!doctors.Any()) return;

            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    PatientName = "منة الله سعيد",
                    PatientPhone = "01096790876",
                    DoctorId = doctors.First(d => d.FullName == "د. خالد المطيري").Id,
                    Status = AppointmentStatus.Pending,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Appointment
                {
                    PatientName = "سارة أحمد",
                    PatientPhone = "01012345678",
                    DoctorId = doctors.First(d => d.FullName == "د. أحمد الشريف").Id,
                    Status = AppointmentStatus.Pending,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Appointment
                {
                    PatientName = "خالد يوسف",
                    PatientPhone = "01234567891",
                    DoctorId = doctors.First(d => d.FullName == "د. يوسف البقمي").Id,
                    Status = AppointmentStatus.Completed,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                }
            };

            context.Appointments.AddRange(appointments);
            await context.SaveChangesAsync();
        }

        private static async Task SeedDevicesAsync(AppDbContext context)
        {
            if (await context.Services.AnyAsync()) return;

            var devices = new List<Service>
            {
                new Service
                {
                    Name = "جهاز أشعة مقطعية 128 شريحة",
                    Description = "جهاز حديث لتصوير الأعضاء الداخلية بدقة عالية",
                    ImageUrl = null
                },
                new Service
                {
                    Name = "جهاز قسطرة قلبية",
                    Description = "لإجراء القسطرة العلاجية والتشخيصية للقلب",
                    ImageUrl = null
                },
                new Service
                {
                    Name = "حضانة أطفال حديثي الولادة",
                    Description = "حضانات مجهزة بالكامل للعناية بالأطفال الخدج",
                    ImageUrl = null
                }
            };

            context.Services.AddRange(devices);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStaffAsync(AppDbContext context)
        {
            if (await context.Staff.AnyAsync()) return;

            var staff = new List<Staff>
            {
                new Staff
                {
                    Name = "محمد عبدالله",
                    Role = "مدير إداري",
                    Description = "مسؤول عن الإدارة العامة لعمليات المستشفى",
                    ImageUrl = null
                },
                new Staff
                {
                    Name = "فاطمة الزهراني",
                    Role = "رئيسة التمريض",
                    Description = "تشرف على فريق التمريض في جميع الأقسام",
                    ImageUrl = null
                },
                new Staff
                {
                    Name = "عمر الشمري",
                    Role = "موظف استقبال",
                    Description = "أول نقطة تواصل مع المرضى والزوار",
                    ImageUrl = null
                }
            };

            context.Staff.AddRange(staff);
            await context.SaveChangesAsync();
        }

        private static async Task SeedContractsAsync(AppDbContext context)
        {
            if (await context.Contracts.AnyAsync()) return;

            var contracts = new List<Contract>
            {
                new Contract { Name = "شركة التأمين الطبي المتحدة", ImageUrl = null },
                new Contract { Name = "شركة توريد الأجهزة الطبية الحديثة", ImageUrl = null },
                new Contract { Name = "شركة الأدوية العالمية", ImageUrl = null }
            };

            context.Contracts.AddRange(contracts);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStatisticsAsync(AppDbContext context)
        {
            if (await context.Statistics.AnyAsync()) return;

            var statistics = new List<Statistic>
            {
                new Statistic { Key = "سرير", Value = "150" },
                new Statistic { Key = "طبيب", Value = "45" },
                new Statistic { Key = "عملية ناجحة", Value = "2500+" },
                new Statistic { Key = "سنوات خبرة", Value = "20" }
            };

            context.Statistics.AddRange(statistics);
            await context.SaveChangesAsync();
        }

        private static async Task SeedContactInfoAsync(AppDbContext context)
        {
            if (await context.ContactInfo.AnyAsync()) return;

            var contactInfo = new ContactInfo
            {
                Email = "info@alshefa-hospital.com",
                Phone = "0138001234",
                WhatsApp = "0501234567",
                Address = "شارع الملك فهد، الفيوم، مصر",
                Hours = "على مدار 24 ساعة",
                MapUrl = "https://maps.google.com/?q=alshefa-hospital"
            };
        }
    }
}
