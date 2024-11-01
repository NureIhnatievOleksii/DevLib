using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Login
{
    public record LoginWithGoogleCommand(
        string UserId,
        string Token
    ) : IRequest<AuthResponseDto>;
}
