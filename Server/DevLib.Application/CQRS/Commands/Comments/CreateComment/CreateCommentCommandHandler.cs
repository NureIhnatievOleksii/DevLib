using MediatR;
using DevLib.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using DevLib.Domain.CommentAggregate;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Commands.Comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IPostRepository postRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                CommentId = Guid.NewGuid(),
                UserId = command.UserId,
                Content = command.Text,
                PostId = command.PostId,
                DateTime = DateTime.UtcNow,
                Post = await _postRepository.GetByIdAsync(command.PostId, cancellationToken),
                User = await _userManager.FindByIdAsync(command.UserId.ToString())
            };

            await _commentRepository.CreateAsync(comment, cancellationToken);

            return comment.CommentId;
        }
    }
}
