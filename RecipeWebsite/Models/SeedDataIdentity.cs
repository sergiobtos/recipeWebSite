using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeWebsite.Models
{
    public static class SeedDataIdentity
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                string adminUser = "Sergio";
                string adminPassword = "Sl20102014@";

                string managerUser = "Paul";
                string managerPassword = "Secret456$";

                string adminRoleName = "Admin";
                string managerRoleName = "Manager";

                RoleManager<IdentityRole> roleManager = app.ApplicationServices.
                                    GetRequiredService<RoleManager<IdentityRole>>();
                IdentityRole adminRole = await roleManager.FindByNameAsync(adminRoleName);

                if (adminRole == null)
                {
                    adminRole = new IdentityRole(adminRoleName);
                    await roleManager.CreateAsync(adminRole);
                }

                IdentityRole managerRole = await roleManager.FindByNameAsync(managerRoleName);

                if (managerRole == null)
                {
                    managerRole = new IdentityRole(managerRoleName);
                    await roleManager.CreateAsync(managerRole);
                }
                UserManager<IdentityUser> userManager = app.ApplicationServices.
                                            GetRequiredService<UserManager<IdentityUser>>();
                IdentityUser admin = await userManager.FindByIdAsync(adminUser);
                if (admin == null)
                {
                    admin = new IdentityUser(adminUser);
                    await userManager.CreateAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, adminRoleName);
                }
                else
                {
                    if(!(await userManager.IsInRoleAsync(admin, adminRoleName)))
                    {
                        await userManager.AddToRoleAsync(admin, adminRoleName);
                    }
                }
                IdentityUser manager = await userManager.FindByIdAsync(managerUser);
                if (manager == null)
                {
                    manager = new IdentityUser(managerUser);
                    await userManager.CreateAsync(manager, managerPassword);
                    await userManager.AddToRoleAsync(manager, managerRoleName);
                }
                else
                {
                    if(!(await userManager.IsInRoleAsync(manager, managerRoleName)))
                    {
                        await userManager.AddToRoleAsync(manager, managerRoleName);
                    }
                }
            }
        }
    }
}
