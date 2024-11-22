using DevLib.Domain.BookAggregate;
using DevLib.Domain.CommentAggregate;
using DevLib.Domain.RatingAggregate;
using DevLib.Domain.TagAggregate;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken);
    Task<IdentityResult> CreateAsync(Book book, CancellationToken cancellationToken);
    Task<IdentityResult> UpdateAsync(Book book, CancellationToken cancellationToken);
    Task DeleteAsync(Book book, CancellationToken cancellationToken);
    Task<Book> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<List<Rating>> GetRatingsByBookIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<List<Comment>> GetCommentsByBookIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<List<Tag>> GetTagsByBookIdAsync(Guid bookId, CancellationToken cancellationToken);

}