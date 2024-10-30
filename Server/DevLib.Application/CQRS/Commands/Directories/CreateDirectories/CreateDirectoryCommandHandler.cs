using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.ArticleAggregate;
using Microsoft.Extensions.Logging;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public class CreateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IArticleRepository articleRepository, IMapper mapper, ILogger<CreateDirectoryCommandHandler> logger)
    : IRequestHandler<CreateDirectoryCommand>
{
    public async Task Handle(CreateDirectoryCommand command, CancellationToken cancellationToken)
    {
        var directory = new DLDirectory
        {
            DirectoryName = command.DirectoryName,
            ImgLink = command.DirectoryImgUrl
        };

        await directoryRepository.CreateAsync(directory, cancellationToken);

        if (command.Articles != null && command.Articles.Any())
        {
            logger.LogInformation("Articles count: {Count}", command.Articles.Count);

            foreach (var articleDto in command.Articles)
            {
                logger.LogInformation("Processing article: {Name}, {Text}", articleDto.Name, articleDto.Text);

                var newArticle = new Article
                {
                    ArticleId = Guid.NewGuid(),
                    DirectoryId = directory.DirectoryId,
                    Text = articleDto.Text,
                    ChapterName = articleDto.Name,
                    Directory = directory
                };

                await articleRepository.CreateAsync(newArticle, cancellationToken);
            }
        }
        else
        {
            logger.LogWarning("No articles were provided or the list is empty.");
        }
    }
}
