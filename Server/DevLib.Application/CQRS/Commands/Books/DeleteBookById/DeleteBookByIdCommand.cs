using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.DeleteBookById;

public record DeleteBookByIdCommand(Guid BookId) : IRequest;