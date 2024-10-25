using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.RatingAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.Repositories;

public class RatingRepository(DevLibContext context) : IRatingRepository
{
    public async Task AddAsync(Rating rating, CancellationToken cancellationToken)
    {
        await context.Ratings.AddAsync(rating, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
