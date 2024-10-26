using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public class CreateBookCommandHandler(IBookRepository repository, IMapper mapper)
    : IRequestHandler<CreateBookCommand>
{
    public async Task Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(command);

        await repository.CreateAsync(book, cancellationToken);
    }
}