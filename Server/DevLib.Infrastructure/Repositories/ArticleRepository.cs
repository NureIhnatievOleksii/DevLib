using DevLib.Infrastructure.Database;
using DevLib.Domain.ArticleAggregate;
using DevLib.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevLib.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DevLibContext _context;

        public ArticleRepository(DevLibContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Article article, CancellationToken cancellationToken = default)
        {
            await _context.Articles.AddAsync(article, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Article> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleId, cancellationToken);
        }

        public async Task UpdateAsync(Article article, CancellationToken cancellationToken = default)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Article>> GetByDirectoryIdAsync(Guid directoryId, CancellationToken cancellationToken)
        {
            return await _context.Articles
                .Where(article => article.DirectoryId == directoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
