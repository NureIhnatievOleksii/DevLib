using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Comments.AddReview;

public class AddReviewCommandHandler(ICommentRepository repository, IMapper mapper)
    : IRequestHandler<AddReviewCommand>
{
    public async Task Handle(AddReviewCommand command, CancellationToken cancellationToken)
    {
        var comment = mapper.Map<Comment>(command);

        await repository.AddReviewAsync(comment, cancellationToken);
    }
}
