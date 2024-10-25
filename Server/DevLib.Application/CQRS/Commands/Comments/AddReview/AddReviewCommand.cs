using MediatR;

namespace DevLib.Application.CQRS.Commands.Comments.AddReview;

public record AddReviewCommand
(
    Guid BookId,
    Guid UserId,
    string Content
) : IRequest;
