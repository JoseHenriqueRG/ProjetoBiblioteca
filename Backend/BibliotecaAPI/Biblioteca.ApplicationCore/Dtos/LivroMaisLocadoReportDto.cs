namespace Biblioteca.ApplicationCore.Dtos
{
    public class LivroMaisLocadoReportDto
    {
        public int LivroId { get; set; }
        public string LivroTitulo { get; set; } = string.Empty;
        public int TotalLocacoes { get; set; }
    }
}
