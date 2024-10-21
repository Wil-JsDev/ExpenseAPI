using ExpenseAPI.Domain.Enum;
using ExpenseAPI.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            User userAdmin = new User
            {
                UserName = "AdminUser",
                Email = "AdminElJefe@gmail.com",
                FirstName = "Wilmer",
                LastName = "Jose",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != userAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(userAdmin.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(userAdmin, "123Pa$$word");
                    await userManager.AddToRoleAsync(userAdmin,Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(userAdmin, Roles.Basic.ToString());
                }

            }

        }
    }
}
