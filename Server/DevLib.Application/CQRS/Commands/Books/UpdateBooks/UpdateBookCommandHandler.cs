using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    : IRequestHandler<UpdateBookCommand>
{
    public async Task Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(command.BookId, cancellationToken)
                       ?? throw new Exception("Book not found");

        mapper.Map(command, book);

        await bookRepository.UpdateAsync(book, cancellationToken);
    }
}
