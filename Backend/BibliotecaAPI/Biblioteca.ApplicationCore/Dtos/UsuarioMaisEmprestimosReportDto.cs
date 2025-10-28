namespace Biblioteca.ApplicationCore.Dtos
{
    public class UsuarioMaisEmprestimosReportDto
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public int TotalEmprestimos { get; set; }
    }
}
