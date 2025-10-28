using Biblioteca.Domain.Models;
using System;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class LocacaoDto
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public string LivroTitulo { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }
        public LocacaoStatus Status { get; set; }
    }
}
