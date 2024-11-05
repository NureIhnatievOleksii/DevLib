using AutoMapper;
using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.DirectoryAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Tags.CreateTags;

public class CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<CreateTagCommand>
{
    public async Task Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        await tagRepository.AddTagConnectionAsync(command.bookId, command.postId, command.tagText, cancellationToken);
    }
}