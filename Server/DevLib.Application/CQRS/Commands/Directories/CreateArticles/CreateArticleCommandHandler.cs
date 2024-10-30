using System;
using System.Collections.Generic;
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

namespace DevLib.Application.CQRS.Commands.Directories.CreateArticles;

public class CreateArticleCommandHandler(IDirectoryRepository directoryRepository, IArticleRepository articleRepository, IMapper mapper)
: IRequestHandler<CreateArticleCommand>
{
    public async Task Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = new Article
        {
            ChapterName = command.ArticleName,
            Text = command.ArticleContent,
            DirectoryId = command.DirectoryId,
            Directory = await directoryRepository.GetByIdAsync(command.DirectoryId)
        };

        await articleRepository.CreateAsync(article, cancellationToken);
    }
}
