using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevLib.Application.CQRS.Commands.Directories.CreateArticles;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.DirectoryAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DevLib.Application.CQRS.Commands.Comments.UpdateComment;

public class UpdateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
: IRequestHandler<UpdateCommentCommand, object>
{
    public async Task<object> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetComment(command.comment_id, cancellationToken);

        if(comment == null)
        {
            return null;
        }

        comment.Content = command.text;

        await commentRepository.UpdateAsync(comment, cancellationToken);
        return comment.CommentId;
    }
}
