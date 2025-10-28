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
        public DateTime DataRetirada { get; set; }
        [Required]
        public DateTime DataDevolucaoPrevista { get; set; }
    }
}
