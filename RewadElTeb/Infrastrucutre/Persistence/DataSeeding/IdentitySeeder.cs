using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.DataSeeding
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            const string adminRole = "Admin";

            // Create Admin Role
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(adminRole));
            }

            // Create Admin Account
            const string adminEmail = "admin@rewadelteb.com";
            const string adminPassword = "Admin@123456";

            var adminUser = await userManager
                .FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    adminUser,
                    adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(
                            ", ",
                            result.Errors.Select(e => e.Description)));
                }
            }

            // Assign Admin Role
            if (!await userManager.IsInRoleAsync(
                adminUser,
                adminRole))
            {
                await userManager.AddToRoleAsync(
                    adminUser,
                    adminRole);
            }
        }
    }
}