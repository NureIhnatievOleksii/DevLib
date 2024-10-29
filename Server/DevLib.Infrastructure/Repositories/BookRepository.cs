using Microsoft.EntityFrameworkCore;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

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
        book.PublicationDateTime = DateTime.Now;

        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");

        if (!Directory.Exists(webRootPath))
        {
            Directory.CreateDirectory(webRootPath);
        }

        string fileName = Path.GetFileName(book.FilePath);
        string destinationPath = Path.Combine(webRootPath, fileName);

        try
        {
            if (File.Exists(destinationPath))
            {
                return IdentityResult.Failed(new IdentityError { Description = "This book was already added" });
            }

            File.Copy(book.FilePath, destinationPath);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.ToString());
            return IdentityResult.Failed(new IdentityError { Description = "File was not found or could not be copied" });
        }

        book.FilePath = $"/Books/{fileName}";

        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
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
