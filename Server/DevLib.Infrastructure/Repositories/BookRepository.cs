using Microsoft.EntityFrameworkCore;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.CommentAggregate;
using DevLib.Domain.RatingAggregate;

namespace DevLib.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DevLibContext _context;

    public BookRepository(DevLibContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Books.ToListAsync(cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }



    public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Books.FirstOrDefaultAsync(c => c.BookId == id, cancellationToken);
    }

    public async Task<IdentityResult> UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync(cancellationToken);


        return IdentityResult.Success;
    }

    public async Task DeleteAsync(Book book, CancellationToken cancellationToken = default)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Rating>> GetRatingsByBookIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return await _context.Ratings.Where(r => r.BookId == bookId).ToListAsync(cancellationToken);
    }

    public async Task<List<Comment>> GetCommentsByBookIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Include(c => c.User)  
            .Where(c => c.BookId == bookId)
            .ToListAsync(cancellationToken);
    }


}
