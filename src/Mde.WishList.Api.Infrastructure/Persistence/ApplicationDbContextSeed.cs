using Mde.WishList.Api.Domain.Entities;
using Mde.WishList.Api.Domain.ValueObjects;
using Mde.WishList.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mde.WishList.Api.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");
            var userRole = new IdentityRole("User");

            var roles = new[] { administratorRole, userRole };

            foreach(var role in roles)
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = new ApplicationUser { Id = "the-administrator-id", UserName = "administrator@localhost", Email = "administrator@localhost" };
            var normalUser = new ApplicationUser { Id = "a-normal-user", UserName = "normal@localhost", Email = "normal@localhost" };

            var users = new[] { adminUser, normalUser };

            
            foreach(var user in users)
            {
                if (userManager.Users.All(u => u.UserName != user.UserName))
                {
                    await userManager.CreateAsync(user, "Seedpassword1!");
                }
            }

            await userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name });
            await userManager.AddToRolesAsync(normalUser, new[] { userRole.Name });

        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Created = DateTime.Now.AddDays(-1),
                    CreatedBy = "a-normal-user",
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesWithoutAutoAuditables();
            }
        }
    }
}
