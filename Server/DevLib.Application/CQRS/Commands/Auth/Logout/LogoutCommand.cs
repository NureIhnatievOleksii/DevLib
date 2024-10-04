using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Logout;

public record LogoutCommand(string Token) : IRequest<AuthResponseDto>;
