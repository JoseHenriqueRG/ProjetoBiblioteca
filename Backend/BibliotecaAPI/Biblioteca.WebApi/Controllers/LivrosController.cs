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
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroDto>>> GetLivros()
        {
            var livros = await _livroService.GetAllAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDto>> GetLivro(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LivroDto>>> SearchLivros([FromQuery] string query)
        {
            var livros = await _livroService.SearchAsync(query);
            return Ok(livros);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<LivroDto>> PostLivro(CreateLivroDto createLivroDto)
        {
            var novoLivro = await _livroService.AddAsync(createLivroDto);
            return CreatedAtAction(nameof(GetLivro), new { id = novoLivro.Id }, novoLivro);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutLivro(int id, UpdateLivroDto updateLivroDto)
        {
            await _livroService.UpdateAsync(id, updateLivroDto);
            return NoContent();
        }
    }
}
