using DevLib.Application.Interfaces.Repositories;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.DeleteBookById;

public class DeleteBookByIdCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<DeleteBookByIdCommand>
{
    public async Task Handle(DeleteBookByIdCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var book = await bookRepository.GetByIdAsync(command.BookId, cancellationToken)
                ?? throw new Exception($"Book with ID {command.BookId} not found.");

            await bookRepository.DeleteAsync(book, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}