using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using Microsoft.AspNetCore.Identity;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository repository, ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<UpdateBookCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(command);
        book.PublicationDateTime = DateTime.UtcNow;

        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");
        if (!Directory.Exists(webRootPath))
        {
            Directory.CreateDirectory(webRootPath);
        }

        if (command.BookImg != null)
        {
            var imageFileName = $"{DateTime.UtcNow.Ticks}_{Path.GetFileName(command.BookImg.FileName)}";
            var imageDestinationPath = Path.Combine(webRootPath, imageFileName);

            try
            {
                using (var stream = new FileStream(imageDestinationPath, FileMode.Create))
                {
                    await command.BookImg.CopyToAsync(stream, cancellationToken);
                }

                book.BookImg = $"/Books/{imageFileName}";
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return IdentityResult.Failed(new IdentityError { Description = "Image file could not be copied" });
            }
        }

        if (command.BookPdf != null)
        {
            var pdfFileName = $"{DateTime.UtcNow.Ticks}_{Path.GetFileName(command.BookPdf.FileName)}";
            var pdfDestinationPath = Path.Combine(webRootPath, pdfFileName);

            try
            {
                using (var stream = new FileStream(pdfDestinationPath, FileMode.Create))
                {
                    await command.BookPdf.CopyToAsync(stream, cancellationToken);
                }

                book.FilePath = $"/Books/{pdfFileName}";
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return IdentityResult.Failed(new IdentityError { Description = "PDF file could not be copied" });
            }
        }

        var result = await repository.UpdateAsync(book, cancellationToken);

        return result;
    }
}
