using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.PostAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DevLib.Infrastructure.Repositories;

public class PostRepository(DevLibContext context) : IPostRepository
{
    public async Task CreateAsync(Post post, CancellationToken cancellationToken = default)
    {
        await context.Posts.AddAsync(post, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Post> GetByIdAsync(Guid postId, CancellationToken cancellationToken = default)
    {
        return await context.Posts.FirstOrDefaultAsync(d => d.PostId == postId, cancellationToken);
    }

    public async Task<IReadOnlyList<Post>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Posts.ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Post post, CancellationToken cancellationToken = default)
    {
        context.Posts.Remove(post);
        await context.SaveChangesAsync(cancellationToken);
    }
}
