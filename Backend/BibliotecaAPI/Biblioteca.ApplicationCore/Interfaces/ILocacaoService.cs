using Biblioteca.ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Interfaces
{
    public interface ILocacaoService
    {
        Task<LocacaoDto> CreateLocacaoAsync(CreateLocacaoDto createLocacaoDto);
        Task<LocacaoDto?> GetLocacaoByIdAsync(int id);
        Task<IEnumerable<LocacaoDto>> GetAllLocacoesAsync();
        Task<IEnumerable<LocacaoDto>> GetLocacoesByUserIdAsync(int userId);
        Task RenovarLocacaoAsync(int id, int diasParaRenovar);
        Task DevolverLocacaoAsync(int id);
        Task<IEnumerable<DevolucaoReportDto>> GetDevolucoesRealizadasReportAsync();
        Task<IEnumerable<LivroMaisLocadoReportDto>> GetLivrosMaisLocadosReportAsync();
        Task<IEnumerable<UsuarioMaisEmprestimosReportDto>> GetUsuariosComMaisEmprestimosReportAsync();
    }
}
