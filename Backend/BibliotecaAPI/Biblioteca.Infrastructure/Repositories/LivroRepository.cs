using Biblioteca.ApplicationCore.Interfaces;
using Biblioteca.Domain.Models;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _context;

        public LivroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<Livro?> GetByIdAsync(int id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task<IEnumerable<Livro>> SearchAsync(string query)
        {
            return await _context.Livros
                .Where(l => l.Titulo.Contains(query) || l.Autor.Contains(query) || l.ISBN.Contains(query))
                .ToListAsync();
        }

        public async Task UpdateAsync(Livro livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
