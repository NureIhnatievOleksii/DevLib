using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public record CreateReplyCommand(Guid UserId, string Text, Guid PostId, Guid CommentId) : IRequest<IdentityResult>;
}