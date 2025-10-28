using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
