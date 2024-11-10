using DevLib.Domain.BookAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Tags.GetBooksByTagText
{
    public record GetBooksByTagTextQuery(string tagText) : IRequest<List<BookNameDto>>;
}