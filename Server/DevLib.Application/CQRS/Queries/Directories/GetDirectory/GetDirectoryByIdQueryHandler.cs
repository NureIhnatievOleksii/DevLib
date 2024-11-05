using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Directories.GetDirectoryById
{
    public class GetDirectoryByIdQueryHandler : IRequestHandler<GetDirectoryByIdQuery, GetDirectoryDto>
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IMapper _mapper;

        public GetDirectoryByIdQueryHandler(IDirectoryRepository directoryRepository, IMapper mapper)
        {
            _directoryRepository = directoryRepository;
            _mapper = mapper;
        }

        public async Task<GetDirectoryDto> Handle(GetDirectoryByIdQuery query, CancellationToken cancellationToken)
        {
            var directory = await _directoryRepository.GetByIdAsync(query.DirectoryId, cancellationToken)
                ?? throw new Exception("Directory not found");

            var articles = await _directoryRepository.GetArticlesByDirectoryIdAsync(query.DirectoryId, cancellationToken);

            var directoryDto = new GetDirectoryDto(
                directory.DirectoryId,
                directory.DirectoryName,
                directory.ImgLink,
                articles.Select(article => new ArticleDto(article.ArticleId, article.ChapterName))
            );

            return directoryDto;
        }
    }
}
