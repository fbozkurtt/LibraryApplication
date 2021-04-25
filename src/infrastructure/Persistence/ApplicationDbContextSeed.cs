﻿using LibraryApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var administrator = new ApplicationUser { UserName = "admin" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "admin");
                await userManager.AddToRolesAsync(administrator, roleManager.Roles.Select(r => r.Name).ToList());
            }
        }

        public static async Task SeedDefaultRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var defaultRoles = Config.GetDefaultRoles();

            foreach (var role in defaultRoles)
            {
                if (await roleManager.FindByNameAsync(role.Name) == null)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
