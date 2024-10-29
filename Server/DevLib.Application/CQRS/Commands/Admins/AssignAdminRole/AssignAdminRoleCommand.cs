using MediatR;

namespace DevLib.Application.CQRS.Commands.Admins.AssignAdminRole
{
    public record AssignAdminRoleCommand(Guid UserId) : IRequest<CommandResultDto>;
}
