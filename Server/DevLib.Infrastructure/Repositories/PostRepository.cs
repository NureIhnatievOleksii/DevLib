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
}
