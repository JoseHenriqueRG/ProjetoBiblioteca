using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            await SeedRolesAndAdmin(serviceProvider);
            await SeedBooks(serviceProvider);
        }

        private static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roleNames = { "Administrador", "UsuarioPadrao" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            if (await userManager.FindByEmailAsync("admin@biblioteca.com") == null)
            {
                var newAdminUser = new Usuario
                {
                    UserName = "admin@biblioteca.com",
                    Email = "admin@biblioteca.com",
                    Nome = "Administrador",
                    Tipo = "Administrador"
                };

                var result = await userManager.CreateAsync(newAdminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Administrador");
                }
            }
        }

        private static async Task SeedBooks(IServiceProvider serviceProvider)
        {
            using (var context = new BibliotecaContext(
                serviceProvider.GetRequiredService<DbContextOptions<BibliotecaContext>>()))
            {
                if (context.Livros.Any())
                {
                    return;   // DB has been seeded
                }

                context.Livros.AddRange(
                    new Livro
                    {
                        Titulo = "O Senhor dos Anéis",
                        Autor = "J.R.R. Tolkien",
                        Editora = "HarperCollins",
                        AnoPublicacao = 1954,
                        ISBN = "978-0261102385",
                        Quantidade = 5
                    },
                    new Livro
                    {
                        Titulo = "O Hobbit",
                        Autor = "J.R.R. Tolkien",
                        Editora = "HarperCollins",
                        AnoPublicacao = 1937,
                        ISBN = "978-0261103283",
                        Quantidade = 3
                    },
                    new Livro
                    {
                        Titulo = "1984",
                        Autor = "George Orwell",
                        Editora = "Secker & Warburg",
                        AnoPublicacao = 1949,
                        ISBN = "978-0451524935",
                        Quantidade = 0
                    },
                    new Livro
                    {
                        Titulo = "Dom Quixote",
                        Autor = "Miguel de Cervantes",
                        Editora = "Francisco de Robles",
                        AnoPublicacao = 1605,
                        ISBN = "978-8420412146",
                        Quantidade = 2
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
