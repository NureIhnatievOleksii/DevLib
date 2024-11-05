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

namespace DevLib.Application.CQRS.Commands.Tags.UpdateTags;

public class UpdateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
: IRequestHandler<UpdateTagCommand>
{
    public async Task Handle(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetTagByIdAsync(command.TagId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(command.Name))
        {
            tag.TagText = command.Name;
        }

        await tagRepository.UpdateAsync(tag, cancellationToken);
    }
}
