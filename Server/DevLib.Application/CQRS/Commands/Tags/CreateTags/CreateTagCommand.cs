using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Tags.CreateTags;

public record CreateTagCommand(
    string tagText,
    Guid? postId,
    Guid? bookId,
    Guid? directoryId
) : IRequest;
