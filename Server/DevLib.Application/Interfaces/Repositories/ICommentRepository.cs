using DevLib.Domain.CommentAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task AddReviewAsync(Comment comment, CancellationToken cancellationToken);
    Task CreateAsync(Comment comment, CancellationToken cancellationToken);
}
