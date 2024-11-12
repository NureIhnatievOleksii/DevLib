using DevLib.Domain.CommentAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.PostAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface IPostRepository
{
    Task CreateAsync(Post post, CancellationToken cancellationToken);
    Task<Post> GetByIdAsync(Guid postId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Post>> GetAllAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Post post, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken);

}
