using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookRepository repository;
    private readonly IMapper mapper;

    public CreateBookCommandHandler(IBookRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(command);

        book.PublicationDateTime = DateTime.UtcNow;

        if (command.BookImg != null)
        {
            var filePath = Path.Combine("Uploads", command.BookImg.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.BookImg.CopyToAsync(stream, cancellationToken);
            }
            // todo saving the file
        }

        if (command.BookPdf != null)
        {
            var pdfPath = Path.Combine("Uploads", command.BookPdf.FileName);
            using (var stream = new FileStream(pdfPath, FileMode.Create))
            {
                await command.BookPdf.CopyToAsync(stream, cancellationToken);
            }
            // todo saving the file
        }

        await repository.CreateAsync(book, cancellationToken);
    }
}
