using DevLib.Domain.PostAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface IPostRepository
{
    Task CreateAsync(Post post, CancellationToken cancellationToken);
}
