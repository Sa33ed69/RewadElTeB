using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.IRepositories;
using Application.Mappings;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.AuthService;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Identity;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.JwtModule;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infrastructure.DependencyInjection
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Repo

            services.AddScoped(
               typeof(IGenericRepository<>),
               typeof(GenericRepository<>));

            //Mappig

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<StaffProfile>();
                cfg.AddProfile<ContractProfile>();
                cfg.AddProfile<StatisticProfile>();
                cfg.AddProfile<ContactInfoProfile>();
                cfg.AddProfile<ServiceProfile>();
                cfg.AddProfile<PatientFeedbackProfile>();
                cfg.AddProfile<AuditLogProfile>();
                cfg.AddProfile<AppointmentProfile>();
            });

            // Identity

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IContactInfoService, ContactInfoService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IPatientFeedbackService, PatientFeedbackService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<AuditLogInterceptor>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddHostedService<AuditLogCleanupService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            //DbContext

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(
                 serviceProvider.GetRequiredService<AuditLogInterceptor>());
            });

            return services;

        }
    }
}