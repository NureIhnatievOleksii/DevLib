using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.Exceptions;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public class UpdateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IArticleRepository articleRepository, IMapper mapper)
    : IRequestHandler<UpdateDirectoryCommand>
{
    public async Task Handle(UpdateDirectoryCommand command, CancellationToken cancellationToken)
    {
        var directory = await directoryRepository.GetByIdAsync(command.DirectoryId, cancellationToken);
        if (directory == null)
        {
            throw new NotFoundException($"Directory with ID {command.DirectoryId} not found.");
        }

        directory.DirectoryName = command.DirectoryName;
        directory.ImgLink = command.DirectoryImgUrl;

        await directoryRepository.UpdateAsync(directory, cancellationToken);

        foreach (var articleDto in command.Articles)
        {
            if (articleDto.ArticleId.HasValue) 
            {
                var article = await articleRepository.GetByIdAsync(articleDto.ArticleId.Value, cancellationToken);
                if (article != null)
                {
                    article.ChapterName = articleDto.Name;
                    article.Text = articleDto.Text;

                    await articleRepository.UpdateAsync(article, cancellationToken);
                }
            }
            else
            {
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
    }
}
