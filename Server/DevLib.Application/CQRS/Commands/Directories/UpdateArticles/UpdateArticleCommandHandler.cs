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

namespace DevLib.Application.CQRS.Commands.Directories.UpdateArticles;

public class UpdateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper)
: IRequestHandler<UpdateArticleCommand>
{
    public async Task Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdAsync(command.ArticleId, cancellationToken);
        if (!string.IsNullOrWhiteSpace(command.ArticleName))
        {
            article.ChapterName = command.ArticleName;
        }

        if (!string.IsNullOrWhiteSpace(command.ArticleContent))
        {
            article.Text = command.ArticleContent;
        }

        await articleRepository.UpdateAsync(article, cancellationToken);
    }
}
