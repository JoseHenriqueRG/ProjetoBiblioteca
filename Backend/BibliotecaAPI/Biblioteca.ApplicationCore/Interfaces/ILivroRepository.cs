using Biblioteca.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro?> GetByIdAsync(int id);
        Task<IEnumerable<Livro>> GetAllAsync();
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task<IEnumerable<Livro>> SearchAsync(string query);
    }
}
