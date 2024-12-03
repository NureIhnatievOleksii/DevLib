using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Domain.CommentAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task AddReviewAsync(Comment comment, CancellationToken cancellationToken);
    Task CreateAsync(Comment comment, CancellationToken cancellationToken);
    Task<IdentityResult> CreateReply(Comment comment, Guid CommentId, CancellationToken cancellationToken);
    Task<IdentityResult> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken);
    Task<CommentDto> GetReplies(CommentDto comment, CancellationToken cancellationToken);
    Task<Comment> GetComment(Guid commentId, CancellationToken cancellationToken);
    Task<List<UserCommentDto>> GetUserCommentAsync(Guid UserId, CancellationToken cancellationToken);
    Task UpdateAsync(Comment Comment, CancellationToken cancellationToken);
}
