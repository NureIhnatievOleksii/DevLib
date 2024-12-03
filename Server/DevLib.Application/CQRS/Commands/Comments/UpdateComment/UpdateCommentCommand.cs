using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Comments.UpdateComment;

public record UpdateCommentCommand(
    Guid comment_id,
    string? text
) : IRequest<object>;
