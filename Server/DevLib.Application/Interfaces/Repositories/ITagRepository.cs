using DevLib.Domain.TagAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task AddTagConnectionAsync(Guid bookId, Guid tagId, CancellationToken cancellationToken);
        Task AddTagConnectionToDirectoryAsync(Guid directoryId, Guid tagId, CancellationToken cancellationToken);
    }
}
