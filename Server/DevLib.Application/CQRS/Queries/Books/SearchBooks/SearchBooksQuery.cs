using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.SearchBooks
{
    public record SearchBooksQuery(string? tag, string? BookName) : IRequest<List<BookNameDto>>;
}
public record BookNameDto
{
    public string BookName { get; init; }
    public Guid BookId { get; init; }
    public string BookImg { get; init; }
}