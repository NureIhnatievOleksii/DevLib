namespace DevLib.Application.CQRS.Dtos.Commands;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
}
