using DevLib.Domain.BookmarkAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface IBookmarkRepository
{
    Task AddBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken);
    Task<List<Bookmark>> GetBookmarksByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Bookmark> GetByIdAsync(Guid bookmarkId, CancellationToken cancellationToken);
    Task DeleteAsync(Bookmark bookmark, CancellationToken cancellationToken);
}
