using MediatR;

namespace DevLib.Application.CQRS.Commands.Admins.BanUser
{
    public record BanUserCommand(Guid UserId, bool IsBanned) : IRequest<bool>;

}
