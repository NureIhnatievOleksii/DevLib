using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
