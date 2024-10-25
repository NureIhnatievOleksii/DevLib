using DevLib.Domain.RatingAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface IRatingRepository
{
    Task AddAsync(Rating rating, CancellationToken cancellationToken);
}
