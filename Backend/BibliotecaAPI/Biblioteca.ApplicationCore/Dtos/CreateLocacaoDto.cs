using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class CreateLocacaoDto
    {
        [Required]
        public int LivroId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public DateTimeOffset DataRetirada { get; set; }
        [Required]
        public DateTimeOffset DataDevolucaoPrevista { get; set; }
    }
}
