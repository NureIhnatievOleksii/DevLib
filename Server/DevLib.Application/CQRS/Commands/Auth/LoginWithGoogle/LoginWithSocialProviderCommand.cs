using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Login
{
    public record LoginWithSocialProviderCommand(
        string UserId,
        string Email,
        string Provider,
        string IdToken
    ) : IRequest<AuthResponseDto>;
}
