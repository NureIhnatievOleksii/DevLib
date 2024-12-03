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
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, IdentityResult>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public CreateReplyCommandHandler(ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateReplyCommand command, CancellationToken cancellationToken)
        { 
            var comment = new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = command.UserId,
                Content = command.Text,
                DateTime = DateTime.UtcNow,
                User = await _userManager.FindByIdAsync(command.UserId.ToString())
            };

            return await _commentRepository.CreateReply(comment, command.CommentId, cancellationToken);

        }
    }
}
