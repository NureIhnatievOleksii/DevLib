using DevLib.Application.CQRS.Dtos.Commands;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Auth
{
    public class LoginWithGitHubCommand : IRequest<AuthResponseDto>
    {
        public string Code { get; set; }
    }
}