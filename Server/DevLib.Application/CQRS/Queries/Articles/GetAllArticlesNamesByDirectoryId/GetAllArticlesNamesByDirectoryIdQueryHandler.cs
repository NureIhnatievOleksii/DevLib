using AutoMapper;
using DevLib.Application.CQRS.Dtos.Queries;
using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Articles.GetAllArticlesNamesByDirectoryId
{
    public class GetArticleNamesByDirectoryIdQueryHandler : IRequestHandler<GetArticleNamesByDirectoryIdQuery, List<GetAllArticlesNamesByDirectoryIdDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleNamesByDirectoryIdQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllArticlesNamesByDirectoryIdDto>> Handle(GetArticleNamesByDirectoryIdQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetByDirectoryIdAsync(request.id, cancellationToken);
            return articles.Select(article => new GetAllArticlesNamesByDirectoryIdDto(article.ChapterName)).ToList();
        }
    }
}
