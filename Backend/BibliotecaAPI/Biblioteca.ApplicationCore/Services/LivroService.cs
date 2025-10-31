using Biblioteca.ApplicationCore.Dtos;
using Biblioteca.ApplicationCore.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<LivroDto> AddAsync(CreateLivroDto createLivroDto)
        {
            var livro = new Livro
            {
                Titulo = createLivroDto.Titulo,
                Autor = createLivroDto.Autor,
                ISBN = createLivroDto.ISBN,
                Editora = createLivroDto.Editora,
                AnoPublicacao = createLivroDto.AnoPublicacao,
                Quantidade = createLivroDto.Quantidade
            };

            await _livroRepository.AddAsync(livro);

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Quantidade = livro.Quantidade
            };
        }

        public async Task<IEnumerable<LivroDto>> GetAllAsync()
        {
            var livros = await _livroRepository.GetAllAsync() ?? [];
            return livros.Select(livro => new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Quantidade = livro.Quantidade
            });
        }

        public async Task<LivroDto?> GetByIdAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            if (livro == null) return null;

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Quantidade = livro.Quantidade
            };
        }

        public async Task<IEnumerable<LivroDto>> SearchAsync(string query)
        {
            var livros = await _livroRepository.SearchAsync(query);
            return livros.Select(livro => new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Quantidade = livro.Quantidade
            });
        }

        public async Task UpdateAsync(int id, UpdateLivroDto updateLivroDto)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            if (livro == null)
            {
                // Or throw a custom not found exception
                return;
            }

            livro.Titulo = updateLivroDto.Titulo;
            livro.Autor = updateLivroDto.Autor;
            livro.ISBN = updateLivroDto.ISBN;
            livro.Editora = updateLivroDto.Editora;
            livro.AnoPublicacao = updateLivroDto.AnoPublicacao;
            livro.Quantidade = updateLivroDto.Quantidade;

            await _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            if (livro != null)
            {
                await _livroRepository.DeleteAsync(livro);
            }
        }
    }
}
