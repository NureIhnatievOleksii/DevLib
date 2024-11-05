using DevLib.Domain.ArticleAggregate;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task CreateAsync(Article article, CancellationToken cancellationToken);
        Task<Article> GetByIdAsync(Guid articleId, CancellationToken cancellationToken);
        Task UpdateAsync(Article article, CancellationToken cancellationToken);
        Task<List<Article>> GetByDirectoryIdAsync(Guid directoryId, CancellationToken cancellationToken);

        Task DeleteAsync(Article article, CancellationToken cancellationToken);
    }
}
