using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Articles.GetArticleById;

public class GetCustomerByIdQueryHandler(IArticleRepository articleRepository, IMapper mapper)
	: IRequestHandler<GetArticleByIdQuery, GetArticleByIdQueryDto>
{
	public async Task<GetArticleByIdQueryDto> Handle(
		GetArticleByIdQuery query, CancellationToken cancellationToken)
	{
		var article = await articleRepository.GetByIdAsync(query.Id, cancellationToken)
					   ?? throw new Exception("Article not found");

		return mapper.Map<GetArticleByIdQueryDto>(article);
	}
}
