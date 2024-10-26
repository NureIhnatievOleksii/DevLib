using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.Exceptions;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public class UpdateDirectoryCommandHandler : IRequestHandler<UpdateDirectoryCommand>
{
    private readonly IDirectoryRepository _directoryRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public UpdateDirectoryCommandHandler(
        IDirectoryRepository directoryRepository,
        IArticleRepository articleRepository,
        IMapper mapper)
    {
        _directoryRepository = directoryRepository;
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateDirectoryCommand command, CancellationToken cancellationToken)
    {
        var directory = await _directoryRepository.GetByIdAsync(command.DirectoryId, cancellationToken);
        if (directory == null) throw new NotFoundException($"Directory with ID {command.DirectoryId} not found.");

        directory.DirectoryName = command.DirectoryName;
        directory.ImgLink = command.DirectoryImgUrl;
        await _directoryRepository.UpdateAsync(directory, cancellationToken);

        var articles = command.Articles;

        if (articles != null && articles.Any())
        {
            foreach (var articleDto in articles)
            {
                if (articleDto.ArticleId.HasValue)
                {
                    var article = await _articleRepository.GetByIdAsync(articleDto.ArticleId.Value, cancellationToken);
                    if (article != null)
                    {
                        article.ChapterName = articleDto.Name;
                        article.Text = articleDto.Text;
                        await _articleRepository.UpdateAsync(article, cancellationToken);
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

                    await _articleRepository.CreateAsync(newArticle, cancellationToken);
                }
            }
        }
    }
}
