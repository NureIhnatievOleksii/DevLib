using MediatR;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public record CreateCommentCommand(Guid UserId, string Text, Guid PostId) : IRequest<Guid>;
}