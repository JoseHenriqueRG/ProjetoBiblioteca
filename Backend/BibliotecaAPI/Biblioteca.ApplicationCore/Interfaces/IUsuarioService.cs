using Biblioteca.ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> CreateUserAsync(CreateUsuarioDto createUserDto);
        Task<UsuarioDto> UpdateUserAsync(int id, UpdateUsuarioDto updateUserDto);
        Task DeleteUserAsync(int id);
        Task<UsuarioDto?> GetUserByIdAsync(int id);
        IEnumerable<UsuarioDto> GetAllUsers();
        Task<(bool, LoginResponseDto?)> LoginAsync(LoginDto loginDto);
    }
}
