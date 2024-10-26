using Microsoft.EntityFrameworkCore;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using DevLib.Infrastructure.Database;

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

    public async Task CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Books.FirstOrDefaultAsync(c => c.BookId == id, cancellationToken);
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Book book, CancellationToken cancellationToken = default)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
