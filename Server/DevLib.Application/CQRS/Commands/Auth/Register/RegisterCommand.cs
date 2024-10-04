using MediatR;
using DevLib.Application.CQRS.Dtos.Commands;

namespace DevLib.Application.CQRS.Commands.Auth.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponseDto>;
