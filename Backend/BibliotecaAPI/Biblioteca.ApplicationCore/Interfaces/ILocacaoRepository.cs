using Biblioteca.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao?> GetByIdAsync(int id);
        Task<IEnumerable<Locacao>> GetAllAsync();
        Task AddAsync(Locacao locacao);
        Task UpdateAsync(Locacao locacao);
        Task<IEnumerable<Locacao>> GetByUserIdAsync(int userId);
    }
}
