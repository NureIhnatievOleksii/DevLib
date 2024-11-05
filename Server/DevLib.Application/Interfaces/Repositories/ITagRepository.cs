using DevLib.Domain.TagAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task AddTagAsync(Tag tag, CancellationToken cancellationToken);
        Task<List<Tag>> GetTagsByBookIdAsync(Guid Id, CancellationToken cancellationToken);
        Task AddTagConnectionAsync(Guid? bookId, Guid? postId, string tag, CancellationToken cancellationToken);
        Task RemoveTagConnectionAsync(Guid bookId, Guid tagId, CancellationToken cancellationToken);
        Task UpdateAsync(Tag tag, CancellationToken cancellationToken);
        Task<Tag> GetTagByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteTagAsync(Tag tag, CancellationToken cancellationToken);
    }
}
