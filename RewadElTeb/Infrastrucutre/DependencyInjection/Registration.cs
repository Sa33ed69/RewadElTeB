using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.IRepositories;
using Application.Mappings;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.AuthService;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Identity;
using Infrastructure.Persistence.JwtModule;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.DependencyInjection
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(
                typeof(IGenericRepository<>),
                typeof(GenericRepository<>));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
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

            return services;
        }
    }
}