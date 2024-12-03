namespace DevLib.Application.CQRS.Dtos.Commands;

public class AuthResponseDto
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }
    public int? StatusCode { get; set; }  // Добавляем поле StatusCode
}

