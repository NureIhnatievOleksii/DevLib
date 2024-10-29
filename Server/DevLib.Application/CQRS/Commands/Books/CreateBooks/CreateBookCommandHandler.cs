using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public class CreateBookCommandHandler(IBookRepository repository, IMapper mapper)
    : IRequestHandler<CreateBookCommand,IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
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
        }

        if (command.FilePath != null)
        {
            var pdfPath = Path.Combine("Uploads", command.FilePath.FileName);
            using (var stream = new FileStream(pdfPath, FileMode.Create))
            {
                await command.FilePath.CopyToAsync(stream, cancellationToken);
            }
        }

        return await repository.CreateAsync(book, cancellationToken); // добавлен return
    }
}
