using BookMillApp_Domain.Enum;
using BookMillApp_Domain.Model;
using BookMillApp_Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Infrastructure.Configuration
{

    public static class RoleSeeder
    {
        public static async Task SeedAdminAndRegular(AppDbContext _context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            await _context.Database.EnsureCreatedAsync();
            if (!_context.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                string userName = "Romeo";
                string adminEmail = "Rome@gmail.com";
                string adminPhone = "08034962686";
                string adminPass = "chemistryB3@";

                var anyUser = new User()
                {
                    UserName = userName,
                    Email = adminEmail,
                    PhoneNumber = adminPhone,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var userInDb = await userManager.FindByEmailAsync(anyUser.Email);
                if (userInDb == null)
                {
                    await userManager.CreateAsync(anyUser, adminPass);
                    await userManager.AddToRoleAsync(anyUser, Role.Admin.ToString());
                }
            }
        }
    }
}
