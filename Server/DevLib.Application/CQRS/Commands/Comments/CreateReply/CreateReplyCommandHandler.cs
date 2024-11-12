using MediatR;
using DevLib.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Domain.CommentAggregate;
using Microsoft.AspNetCore.Identity;
using DevLib.Application.CQRS.Commands.Tags.DeleteTagsFromBook;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

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
                DateTime = DateTime.UtcNow
            };

            return await _commentRepository.CreateReply(comment, command.CommentId, cancellationToken);

        }
    }
}
