using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Rating.AddRating;

public class AddRatingCommandHandler(IRatingRepository repository, IMapper mapper)
    : IRequestHandler<AddRatingCommand>
{
    public async Task Handle(AddRatingCommand command, CancellationToken cancellationToken)
    {
        var rating = mapper.Map<DevLib.Domain.RatingAggregate.Rating>(command);

        await repository.AddAsync(rating, cancellationToken);
    }
}
