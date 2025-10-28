using System;

namespace Biblioteca.Domain.Models
{
    public enum LocacaoStatus
    {
        Pendente,
        Devolvido
    }

    public class Locacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoReal { get; set; }
        public LocacaoStatus Status { get; set; }
        public decimal Multa { get; set; }
    }
}
