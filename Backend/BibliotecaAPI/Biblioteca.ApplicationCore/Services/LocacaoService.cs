using Biblioteca.ApplicationCore.Dtos;
using Biblioteca.ApplicationCore.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public LocacaoService(ILocacaoRepository locacaoRepository, ILivroRepository livroRepository, IUsuarioService usuarioService, IConfiguration configuration)
        {
            _locacaoRepository = locacaoRepository;
            _livroRepository = livroRepository;
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        public async Task<LocacaoDto> CreateLocacaoAsync(CreateLocacaoDto createLocacaoDto)
        {
            var livro = await _livroRepository.GetByIdAsync(createLocacaoDto.LivroId);
            if (livro == null || livro.Quantidade <= 0)
            {
                throw new InvalidOperationException("Livro indisponível para locação.");
            }

            var usuario = await _usuarioService.GetUserByIdAsync(createLocacaoDto.UsuarioId);
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            livro.Quantidade--;
            await _livroRepository.UpdateAsync(livro);

            var locacao = new Locacao
            {
                LivroId = createLocacaoDto.LivroId,
                UsuarioId = createLocacaoDto.UsuarioId,
                DataRetirada = createLocacaoDto.DataRetirada,
                DataDevolucaoPrevista = createLocacaoDto.DataDevolucaoPrevista,
                Status = LocacaoStatus.Pendente
            };

            await _locacaoRepository.AddAsync(locacao);

            return new LocacaoDto
            {
                Id = locacao.Id,
                LivroId = locacao.LivroId,
                LivroTitulo = livro.Titulo,
                UsuarioId = locacao.UsuarioId,
                UsuarioNome = usuario.Nome,
                DataRetirada = locacao.DataRetirada,
                DataDevolucaoPrevista = locacao.DataDevolucaoPrevista,
                DataDevolucaoReal = locacao.DataDevolucaoReal,
                Status = locacao.Status.ToString()
            };
        }

        public async Task DevolverLocacaoAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
            {
                throw new KeyNotFoundException("Locação não encontrada.");
            }

            if (locacao.Status == LocacaoStatus.Devolvido)
            {
                throw new InvalidOperationException("Livro já devolvido.");
            }

            locacao.Status = LocacaoStatus.Devolvido;
            locacao.DataDevolucaoReal = DateTime.Now;

            // Calculate fine
            if (locacao.DataDevolucaoReal > locacao.DataDevolucaoPrevista)
            {
                var daysOverdue = (locacao.DataDevolucaoReal.Value - locacao.DataDevolucaoPrevista).Days;
                var dailyFineRate = _configuration.GetValue<decimal>("Locacao:DailyFineRate");
                locacao.Multa = daysOverdue * dailyFineRate;
            }

            var livro = await _livroRepository.GetByIdAsync(locacao.LivroId);
            if (livro != null)
            {
                livro.Quantidade++;
                await _livroRepository.UpdateAsync(livro);
            }

            await _locacaoRepository.UpdateAsync(locacao);
        }

        public async Task<IEnumerable<LocacaoDto>> GetAllLocacoesAsync()
        {
            var locacoes = await _locacaoRepository.GetAllAsync();
            return locacoes.Select(l => new LocacaoDto
            {
                Id = l.Id,
                LivroId = l.LivroId,
                LivroTitulo = l.Livro.Titulo,
                UsuarioId = l.UsuarioId,
                UsuarioNome = l.Usuario.Nome,
                DataRetirada = l.DataRetirada,
                DataDevolucaoPrevista = l.DataDevolucaoPrevista,
                DataDevolucaoReal = l.DataDevolucaoReal,
                Status = l.Status.ToString()
            });
        }

        public async Task<LocacaoDto?> GetLocacaoByIdAsync(int id)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null) return null;

            return new LocacaoDto
            {
                Id = locacao.Id,
                LivroId = locacao.LivroId,
                LivroTitulo = locacao.Livro.Titulo,
                UsuarioId = locacao.UsuarioId,
                UsuarioNome = locacao.Usuario.Nome,
                DataRetirada = locacao.DataRetirada,
                DataDevolucaoPrevista = locacao.DataDevolucaoPrevista,
                DataDevolucaoReal = locacao.DataDevolucaoReal,
                Status = locacao.Status.ToString()
            };
        }

        public async Task<IEnumerable<LocacaoDto>> GetLocacoesByUserIdAsync(int userId)
        {
            var locacoes = await _locacaoRepository.GetByUserIdAsync(userId);
            return locacoes.Select(l => new LocacaoDto
            {
                Id = l.Id,
                LivroId = l.LivroId,
                LivroTitulo = l.Livro.Titulo,
                UsuarioId = l.UsuarioId,
                UsuarioNome = l.Usuario.Nome,
                DataRetirada = l.DataRetirada,
                DataDevolucaoPrevista = l.DataDevolucaoPrevista,
                DataDevolucaoReal = l.DataDevolucaoReal,
                Status = l.Status.ToString()
            });
        }

        public async Task RenovarLocacaoAsync(int id, int diasParaRenovar)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null)
            {
                throw new KeyNotFoundException("Locação não encontrada.");
            }

            if (locacao.Status == LocacaoStatus.Devolvido)
            {
                throw new InvalidOperationException("Não é possível renovar um livro já devolvido.");
            }

            locacao.DataDevolucaoPrevista = locacao.DataDevolucaoPrevista.AddDays(diasParaRenovar);
            await _locacaoRepository.UpdateAsync(locacao);
        }

        public async Task<IEnumerable<DevolucaoReportDto>> GetDevolucoesRealizadasReportAsync()
        {
            var devolucoes = await _locacaoRepository.GetAllAsync();
            return devolucoes
                .Where(l => l.Status == LocacaoStatus.Devolvido)
                .Select(l => new DevolucaoReportDto
                {
                    LocacaoId = l.Id,
                    LivroTitulo = l.Livro.Titulo,
                    UsuarioNome = l.Usuario.Nome,
                    DataRetirada = l.DataRetirada,
                    DataDevolucaoPrevista = l.DataDevolucaoPrevista,
                    DataDevolucaoReal = l.DataDevolucaoReal,
                    Multa = l.Multa
                });
        }

        public async Task<IEnumerable<LivroMaisLocadoReportDto>> GetLivrosMaisLocadosReportAsync()
        {
            var locacoes = await _locacaoRepository.GetAllAsync();
            return locacoes
                .GroupBy(l => l.LivroId)
                .Select(g => new LivroMaisLocadoReportDto
                {
                    LivroId = g.Key,
                    LivroTitulo = g.First().Livro.Titulo,
                    TotalLocacoes = g.Count()
                })
                .OrderByDescending(r => r.TotalLocacoes);
        }

        public async Task<IEnumerable<UsuarioMaisEmprestimosReportDto>> GetUsuariosComMaisEmprestimosReportAsync()
        {
            var locacoes = await _locacaoRepository.GetAllAsync();
            return locacoes
                .GroupBy(l => l.UsuarioId)
                .Select(g => new UsuarioMaisEmprestimosReportDto
                {
                    UsuarioId = g.Key,
                    UsuarioNome = g.First().Usuario.Nome,
                    TotalEmprestimos = g.Count()
                })
                .OrderByDescending(r => r.TotalEmprestimos);
        }
    }
}
