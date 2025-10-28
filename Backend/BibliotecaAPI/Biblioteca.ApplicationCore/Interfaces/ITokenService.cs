using Biblioteca.Domain.Models;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Usuario usuario);
    }
}
