using Biblioteca.ApplicationCore.Dtos;
using Biblioteca.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class LocacoesController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacoesController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpPost]
        public async Task<ActionResult<LocacaoDto>> CreateLocacao(CreateLocacaoDto createLocacaoDto)
        {
            try
            {
                var locacao = await _locacaoService.CreateLocacaoAsync(createLocacaoDto);
                return CreatedAtAction(nameof(GetLocacao), new { id = locacao.Id }, locacao);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocacaoDto>> GetLocacao(int id)
        {
            var locacao = await _locacaoService.GetLocacaoByIdAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }
            return Ok(locacao);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")] 
        public async Task<ActionResult<IEnumerable<LocacaoDto>>> GetAllLocacoes()
        {
            var locacoes = await _locacaoService.GetAllLocacoesAsync();
            return Ok(locacoes);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<LocacaoDto>>> GetLocacoesByUserId(int userId)
        {
            var locacoes = await _locacaoService.GetLocacoesByUserIdAsync(userId);
            return Ok(locacoes);
        }

        [HttpPut("{id}/renovar")]
        public async Task<IActionResult> RenovarLocacao(int id, [FromQuery] int diasParaRenovar)
        {
            try
            {
                await _locacaoService.RenovarLocacaoAsync(id, diasParaRenovar);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> DevolverLocacao(int id)
        {
            try
            {
                var response = await _locacaoService.DevolverLocacaoAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("reports/devolucoes-realizadas")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<DevolucaoReportDto>>> GetDevolucoesRealizadasReport()
        {
            var report = await _locacaoService.GetDevolucoesRealizadasReportAsync();
            return Ok(report);
        }

        [HttpGet("reports/livros-mais-locados")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<LivroMaisLocadoReportDto>>> GetLivrosMaisLocadosReport()
        {
            var report = await _locacaoService.GetLivrosMaisLocadosReportAsync();
            return Ok(report);
        }

        [HttpGet("reports/usuarios-mais-emprestimos")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<UsuarioMaisEmprestimosReportDto>>> GetUsuariosComMaisEmprestimosReport()
        {
            var report = await _locacaoService.GetUsuariosComMaisEmprestimosReportAsync();
            return Ok(report);
        }
    }
}
