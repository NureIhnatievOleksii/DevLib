using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.LastPublishedBooks
{
    public record LastPublishedBooksQuery() : IRequest<List<LastPublishedBookDto>>;
}

public record LastPublishedBookDto
{
    public Guid BookId { get; init; }
    public string BookName { get; init; }
    public string PhotoBook { get; init; }
}