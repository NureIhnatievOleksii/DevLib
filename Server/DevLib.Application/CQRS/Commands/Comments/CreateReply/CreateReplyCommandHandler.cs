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
using EmailService;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, IdentityResult>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public CreateReplyCommandHandler(ICommentRepository commentRepository, UserManager<User> userManager, IEmailSender emailSender)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> Handle(CreateReplyCommand command, CancellationToken cancellationToken)
        {
            var repliedComment = await _commentRepository.GetByIdAsync(command.CommentId, cancellationToken);

            var responder = await _userManager.FindByIdAsync(command.UserId.ToString());
            var author = await _userManager.FindByIdAsync(repliedComment.UserId.ToString());
            var comment = new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = command.UserId,
                Content = command.Text,
                DateTime = DateTime.UtcNow,
                User = responder
            };

            var subject = "Comment Reply";
            var body = @$"{responder.UserName} replied to your comment:
            Сomment:
            {repliedComment.Content}

            Reply:
            {command.Text}
            ";
            var message = new Message(new[] { author.Email }, subject, body);

            await _emailSender.SendEmail(message);

            return await _commentRepository.CreateReply(comment, command.CommentId, cancellationToken);

        }
    }
}
