using TaskTwo.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TaskTwo.Data
{
    public class AdminInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var role = "user";
            var name = "admin";
            var email = "admin@tut.by";
            var password = "adm123";

            if (await roleManager.FindByNameAsync(role) == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = role });
            }

            if (await userManager.FindByNameAsync(name) == null)
            {
                var admin = new User { UserName = name, Email = email };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, role);
                }
            }
        }
    }
}