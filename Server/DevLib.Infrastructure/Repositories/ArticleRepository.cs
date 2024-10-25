using DevLib.Infrastructure.Database;
using DevLib.Domain.ArticleAggregate;
using DevLib.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
