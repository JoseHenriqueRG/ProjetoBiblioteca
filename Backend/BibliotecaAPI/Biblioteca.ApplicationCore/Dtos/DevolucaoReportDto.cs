using System;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class DevolucaoReportDto
    {
        public int LocacaoId { get; set; }
        public string LivroTitulo { get; set; } = string.Empty;
        public string UsuarioNome { get; set; } = string.Empty;
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }
        public decimal Multa { get; set; }
    }
}
