using Biblioteca.ApplicationCore.Interfaces;
using Biblioteca.Domain.Models;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly BibliotecaContext _context;

        public LocacaoRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Locacao locacao)
        {
            await _context.Locacoes.AddAsync(locacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Locacao>> GetAllAsync()
        {
            return await _context.Locacoes
                .Include(l => l.Livro)
                .Include(l => l.Usuario)
                .ToListAsync();
        }

        public async Task<Locacao?> GetByIdAsync(int id)
        {
            return await _context.Locacoes
                .Include(l => l.Livro)
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Locacao>> GetByUserIdAsync(int userId)
        {
            return await _context.Locacoes
                .Include(l => l.Livro)
                .Include(l => l.Usuario)
                .Where(l => l.UsuarioId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Locacao locacao)
        {
            _context.Entry(locacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
