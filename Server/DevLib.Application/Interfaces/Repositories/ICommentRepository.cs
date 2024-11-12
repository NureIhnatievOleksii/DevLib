using DevLib.Domain.CommentAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLib.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task AddReviewAsync(Comment comment, CancellationToken cancellationToken);
    Task CreateAsync(Comment comment, CancellationToken cancellationToken);
    Task<IdentityResult> CreateReply(Comment comment, Guid CommentId, CancellationToken cancellationToken);
}
