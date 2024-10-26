using DevLib.Domain.BookmarkAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface IBookmarkRepository
{
    Task AddBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken);
}
