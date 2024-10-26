using DevLib.Application.CQRS.Dtos.Queries;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Articles.GetArticleById
{
    public record GetArticleByIdQuery(Guid Id) : IRequest<GetArticleByIdQueryDto>;
}