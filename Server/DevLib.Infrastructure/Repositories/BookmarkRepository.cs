using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookmarkAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DevLib.Infrastructure.Repositories;

public class BookmarkRepository(DevLibContext context) : IBookmarkRepository
{
    public async Task AddBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken)
    {
        await context.Bookmarks.AddAsync(bookmark, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Bookmark>> GetBookmarksByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Bookmarks
            .Where(b => b.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
