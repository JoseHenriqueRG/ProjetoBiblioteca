using System;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class DevolucaoReportDto
    {
        public int LocacaoId { get; set; }
        public string LivroTitulo { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
        public DateTimeOffset DataRetirada { get; set; }
        public DateTimeOffset DataDevolucaoPrevista { get; set; }
        public DateTimeOffset? DataDevolucaoReal { get; set; }
        public decimal Multa { get; set; }
    }
}
