using MediatR;

namespace DevLib.Application.CQRS.Commands.Users.ResetUserPassword;

public record ResetUserPasswordCommand(Guid UserId, string NewPassword) : IRequest;
