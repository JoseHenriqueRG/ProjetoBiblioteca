using System.Text.Json.Serialization;

namespace Biblioteca.ApplicationCore.Dtos;

public class LoginResponseDto
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("user")]
    public UsuarioDto User { get; set; }
}
