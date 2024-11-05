using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.DirectoryAggregate;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface IDirectoryRepository
    {
        Task<IReadOnlyList<DLDirectory>> GetAllAsync(CancellationToken cancellationToken);
        Task<DLDirectory> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(DLDirectory directory, CancellationToken cancellationToken);
        Task UpdateAsync(DLDirectory directory, CancellationToken cancellationToken);
        Task DeleteAsync(DLDirectory directory, CancellationToken cancellationToken);
        Task<IReadOnlyList<Article>> GetArticlesByDirectoryIdAsync(Guid directoryId, CancellationToken cancellationToken);
        Task<IReadOnlyList<DLDirectory>> SearchByNameAsync(string name, CancellationToken cancellationToken);

    }
}
