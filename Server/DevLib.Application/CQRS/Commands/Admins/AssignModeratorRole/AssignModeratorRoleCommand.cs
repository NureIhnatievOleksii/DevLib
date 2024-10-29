using MediatR;

namespace DevLib.Application.CQRS.Commands.Admins.AssignModeratorRole
{
    public record AssignModeratorRoleCommand(Guid UserId) : IRequest<CommandResultDto>;
}
public class CommandResultDto
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    public CommandResultDto(bool isSuccess, string errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
}