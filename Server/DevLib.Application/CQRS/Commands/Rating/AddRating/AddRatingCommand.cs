using MediatR;

namespace DevLib.Application.CQRS.Commands.Rating.AddRating;

public record AddRatingCommand
(
    Guid UserId,
    Guid BookId,
    int rate
) : IRequest;
