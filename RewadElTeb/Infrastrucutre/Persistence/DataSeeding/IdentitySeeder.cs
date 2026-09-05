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
            // =========================
            // Create Roles
            // =========================

            const string adminRole = "Admin";
            const string managerRole = "Manager";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(adminRole));
            }

            if (!await roleManager.RoleExistsAsync(managerRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(managerRole));
            }


            // =========================
            // Create Admin Account
            // =========================

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


            // =========================
            // Create Manager Account
            // =========================

            const string managerEmail = "manager@rewadelteb.com";
            const string managerPassword = "Manager@123456";

            var managerUser = await userManager
                .FindByEmailAsync(managerEmail);

            if (managerUser == null)
            {
                managerUser = new ApplicationUser
                {
                    UserName = managerEmail,
                    Email = managerEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    managerUser,
                    managerPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(
                            ", ",
                            result.Errors.Select(e => e.Description)));
                }
            }

            // Assign Manager Role
            if (!await userManager.IsInRoleAsync(
                managerUser,
                managerRole))
            {
                await userManager.AddToRoleAsync(
                    managerUser,
                    managerRole);
            }
        }
    }
}