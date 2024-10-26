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
        Task<IReadOnlyList<DLDirectory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DLDirectory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(DLDirectory directory, CancellationToken cancellationToken = default);
        Task UpdateAsync(DLDirectory directory, CancellationToken cancellationToken = default);
        Task DeleteAsync(DLDirectory directory, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Article>> GetArticlesByDirectoryIdAsync(Guid directoryId, CancellationToken cancellationToken = default);

    }
}
