using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Login
{
    public record LoginWithGoogleCommand(
        string Id,
        string Email,
        string Provider,
        string IdToken
    ) : IRequest<AuthResponseDto>;
}
