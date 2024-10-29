using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.SearchBooks
{
    public record SearchBooksQuery(string BookName) : IRequest<List<BookDto>>;
}
public record BookDto
{
    public Guid BookId { get; init; }
    public string BookName { get; init; }
    public string Author { get; init; }
    public string FilePath { get; init; }
    public DateTime PublicationDateTime { get; set; }
}