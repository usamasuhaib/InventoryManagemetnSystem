using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Helper
{
    public static class DefaultInitializer
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Ensure the default tenants exist
                var tenant1 = await dbContext.Tenants.FirstOrDefaultAsync(t => t.Name == "ABC Company");
                var tenant2 = await dbContext.Tenants.FirstOrDefaultAsync(t => t.Name == "XYZ Company");

                if (tenant1 == null || tenant2 == null)
                {
                    throw new Exception("Tenants not found in the database. Please ensure tenants are seeded correctly.");
                }

                string[] roleNames = { "Admin", "Manager" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var admin1 = new ApplicationUser
                {
                    UserName = "admin1@tenant1.com",
                    Email = "admin1@tenant1.com",
                    FullName = "Admin 1",
                    TenantId = tenant1.Id
                };

                var admin2 = new ApplicationUser
                {
                    UserName = "admin2@tenant2.com",
                    Email = "admin2@tenant2.com",
                    FullName = "Admin 2",
                    TenantId = tenant2.Id
                };

                var manager1 = new ApplicationUser
                {
                    UserName = "manager1@tenant1.com",
                    Email = "manager1@tenant1.com",
                    FullName = "Manager 1",
                    TenantId = tenant1.Id
                };

                var manager2 = new ApplicationUser
                {
                    UserName = "manager2@manager2.com",
                    Email = "manager2@manager2.com",
                    FullName = "Manager 2",
                    TenantId = tenant2.Id
                };

                string adminPassword = "Admin@1234";
                string managerPassword = "Manager@1234";


                var adminUser1 = await userManager.FindByEmailAsync(admin1.Email);
                var adminUser2 = await userManager.FindByEmailAsync(admin2.Email);

                var managerUser1 = await userManager.FindByEmailAsync(manager1.Email);
                var managerUser2 = await userManager.FindByEmailAsync(manager2.Email);


                if (adminUser1 == null)
                {
                    var createAdmin1 = await userManager.CreateAsync(admin1, adminPassword);
                    if (createAdmin1.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin1, "Admin");
                    }
                }

                if (adminUser2 == null)
                {
                    var createAdmin2 = await userManager.CreateAsync(admin2, adminPassword);
                    if (createAdmin2.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin2, "Admin");
                    }
                }


                if (managerUser1 == null)
                {
                    var createManager1 = await userManager.CreateAsync(manager1, managerPassword);
                    if (createManager1.Succeeded)
                    {
                        await userManager.AddToRoleAsync(manager1, "Manager");
                    }
                }

                if (managerUser2 == null)
                {
                    var createManager2 = await userManager.CreateAsync(manager2, managerPassword);
                    if (createManager2.Succeeded)
                    {
                        await userManager.AddToRoleAsync(manager2, "Manager");
                    }
                }
            }
        }
    }

}
