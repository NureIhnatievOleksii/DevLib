using DevLib.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Comments.DeleteCommentById
{
    public class DeleteCommentByIdCommandHandler : IRequestHandler<DeleteCommentByIdCommand, IdentityResult>
    {
        private readonly ICommentRepository commentRepository;

        public DeleteCommentByIdCommandHandler(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<IdentityResult> Handle(DeleteCommentByIdCommand request, CancellationToken cancellationToken)
        {
            return await commentRepository.DeleteCommentAsync(request.CommentId, cancellationToken);
        }
    }
}
