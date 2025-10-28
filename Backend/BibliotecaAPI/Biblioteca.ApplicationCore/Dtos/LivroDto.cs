namespace Biblioteca.ApplicationCore.Dtos
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}
