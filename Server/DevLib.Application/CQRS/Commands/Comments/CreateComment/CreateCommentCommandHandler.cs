using MediatR;
using DevLib.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Domain.CommentAggregate;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Guid> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = command.UserId,
                Content = command.Text,
                PostId = command.PostId,
                DateTime = DateTime.UtcNow
            };

            await _commentRepository.CreateAsync(comment, cancellationToken);

            return comment.CommentId;
        }
    }
}
