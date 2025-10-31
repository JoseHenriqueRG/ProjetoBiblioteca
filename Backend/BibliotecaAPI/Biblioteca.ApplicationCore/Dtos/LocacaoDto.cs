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
        public DateTimeOffset DataRetirada { get; set; }
        public DateTimeOffset DataDevolucaoPrevista { get; set; }
        public DateTimeOffset? DataDevolucaoReal { get; set; }
        public string Status { get; set; }
    }
}
