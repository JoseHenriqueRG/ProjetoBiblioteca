namespace Biblioteca.Domain.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
