using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Books.GetBookById
{
    public record GetBookByIdQuery(Guid Id) : IRequest<GetBookByIdQueryDto>;
}
