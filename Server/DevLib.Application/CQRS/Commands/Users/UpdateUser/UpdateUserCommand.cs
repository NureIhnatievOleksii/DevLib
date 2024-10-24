using MediatR;

namespace DevLib.Application.CQRS.Commands.Users.UpdateUser;
    public record UpdateUserCommand
    (
        Guid UserId,
        string Email,
        string UserName,
        string Photo
    ) : IRequest;
