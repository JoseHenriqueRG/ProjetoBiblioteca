using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Domain.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}
