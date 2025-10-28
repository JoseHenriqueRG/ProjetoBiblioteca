using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class UpdateUsuarioDto
    {        
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Senha { get; set; }
        public string Telefone { get; set; } = string.Empty;
        [Required]
        public string Tipo { get; set; } = string.Empty;
    }
}
