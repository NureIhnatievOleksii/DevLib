using MediatR;
using DevLib.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Domain.CommentAggregate;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, IdentityResult>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateReplyCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IdentityResult> Handle(CreateReplyCommand command, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = command.UserId,
                Content = command.Text,
                PostId = command.PostId,
                DateTime = DateTime.UtcNow
            };

            await _commentRepository.CreateReply(comment,command.CommentId, cancellationToken);

            return IdentityResult.Success;
        }
    }
}
