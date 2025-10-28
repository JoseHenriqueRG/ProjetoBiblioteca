using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data
{
    public class BibliotecaContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Locacao>()
                .HasOne(l => l.Livro)
                .WithMany()
                .HasForeignKey(l => l.LivroId);

            builder.Entity<Locacao>()
                .HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId);
        }
    }
}
