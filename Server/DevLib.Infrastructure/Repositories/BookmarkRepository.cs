using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookmarkAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.Repositories;

public class BookmarkRepository(DevLibContext context) : IBookmarkRepository
{
    public async Task AddBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken)
    {
        await context.Bookmarks.AddAsync(bookmark, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

}
