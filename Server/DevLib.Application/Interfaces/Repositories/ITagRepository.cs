using DevLib.Domain.TagAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task AddTagAsync(Tag tag, CancellationToken cancellationToken);
        Task<List<Tag>> GetTagsByBookIdAsync(Guid Id, CancellationToken cancellationToken);
        Task AddTagConnectionAsync(Guid bookId, string tag, CancellationToken cancellationToken);
    }
}
