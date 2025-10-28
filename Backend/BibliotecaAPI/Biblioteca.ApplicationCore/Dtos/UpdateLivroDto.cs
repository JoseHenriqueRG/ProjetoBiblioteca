using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ApplicationCore.Dtos
{
    public class UpdateLivroDto
    {
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Autor { get; set; } = string.Empty;
        [Required]
        public string ISBN { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }
    }
}
