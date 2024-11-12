using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Comments.DeleteCommentById;

public class DeleteCommentByIdCommand : IRequest<IdentityResult>
{
    public Guid CommentId { get; set; }

    public DeleteCommentByIdCommand(Guid CommentId)
    {
        this.CommentId = CommentId;
    }
}