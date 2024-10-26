using Microsoft.EntityFrameworkCore;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Infrastructure.Database;
using DevLib.Domain.ArticleAggregate;

namespace DevLib.Infrastructure.Repositories
{
    public class DirectoryRepository : IDirectoryRepository
    {
        private readonly DevLibContext _context;

        public DirectoryRepository(DevLibContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<DLDirectory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Directories.ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(DLDirectory directory, CancellationToken cancellationToken = default)
        {
            await _context.Directories.AddAsync(directory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<DLDirectory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Directories.FirstOrDefaultAsync(d => d.DirectoryId == id, cancellationToken);
        }

        public async Task UpdateAsync(DLDirectory directory, CancellationToken cancellationToken = default)
        {
            _context.Directories.Update(directory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(DLDirectory directory, CancellationToken cancellationToken = default)
        {
            _context.Directories.Remove(directory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Article>> GetArticlesByDirectoryIdAsync(Guid directoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Articles
                .Where(article => article.DirectoryId == directoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
