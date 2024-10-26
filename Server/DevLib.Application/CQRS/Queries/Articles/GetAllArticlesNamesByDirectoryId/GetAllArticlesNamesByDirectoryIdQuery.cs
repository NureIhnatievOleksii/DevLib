using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Articles.GetAllArticlesNamesByDirectoryId
{
    public record GetArticleNamesByDirectoryIdQuery(Guid id) : IRequest<List<GetAllArticlesNamesByDirectoryIdDto>>;
}
