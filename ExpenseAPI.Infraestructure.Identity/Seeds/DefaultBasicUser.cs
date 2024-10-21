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
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            User Defaultuser = new User 
            { 
                UserName = "basicUser",
                Email = "basicuser@gmail.com",
                FirstName = "Alejandro",
                LastName = "Perez",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(user => user.Id != Defaultuser.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(Defaultuser.Email);
                if (userEmail == null)
                {
                    await userManager.CreateAsync(Defaultuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(Defaultuser,Roles.Basic.ToString());

                }
            }
        }

    }
}
