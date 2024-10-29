using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public record CreateBookCommand
(
    string BookName,
    string Author,
    string FilePath
) : IRequest;