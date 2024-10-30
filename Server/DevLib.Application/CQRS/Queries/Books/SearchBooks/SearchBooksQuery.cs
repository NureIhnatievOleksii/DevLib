using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.SearchBooks
{
    public record SearchBooksQuery(string BookName) : IRequest<List<BookNameDto>>;
}
public record BookNameDto
{
    public string BookName { get; init; }
}