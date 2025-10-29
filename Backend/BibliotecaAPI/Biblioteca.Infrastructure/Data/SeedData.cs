
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roleNames = { "Administrador", "UsuarioPadrao" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@biblioteca.com");
            if (adminUser == null)
            {
                var newAdminUser = new Usuario
                {
                    UserName = "admin@biblioteca.com",
                    Email = "admin@biblioteca.com",
                    Nome = "Administrador",
                    Tipo = "Administrador"
                };

                var createUserResult = await userManager.CreateAsync(newAdminUser, "Admin@123");
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Administrador");
                }
            }
        }
    }
}
