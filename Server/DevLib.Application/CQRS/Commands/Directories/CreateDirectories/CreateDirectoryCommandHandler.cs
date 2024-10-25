using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.ArticleAggregate;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public class CreateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IArticleRepository articleRepository, IMapper mapper)
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

        foreach (var articleDto in command.Articles)
        {
            var article = new Article
            {
                ArticleId = Guid.NewGuid(),
                DirectoryId = directory.DirectoryId,
                Text = articleDto.Text,
                ChapterName = articleDto.Name,
                Directory = directory
            };

            await articleRepository.CreateAsync(article, cancellationToken);
        }
    }
}
