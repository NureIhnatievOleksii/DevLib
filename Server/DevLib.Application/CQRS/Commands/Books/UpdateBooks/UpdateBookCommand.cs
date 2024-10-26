using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public record UpdateBookCommand
(
    Guid BookId,
    string BookName,
    string Author,
    string FilePath
) : IRequest;