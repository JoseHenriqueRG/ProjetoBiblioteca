using Biblioteca.ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface ILivroService
    {
        Task<LivroDto?> GetByIdAsync(int id);
        Task<IEnumerable<LivroDto>> GetAllAsync();
        Task<LivroDto> AddAsync(CreateLivroDto createLivroDto);
        Task UpdateAsync(int id, UpdateLivroDto updateLivroDto);
        Task<IEnumerable<LivroDto>> SearchAsync(string query);
    }
}
