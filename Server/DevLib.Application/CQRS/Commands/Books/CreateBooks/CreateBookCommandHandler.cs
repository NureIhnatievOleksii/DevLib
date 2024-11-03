using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public class CreateBookCommandHandler(IBookRepository repository,ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<CreateBookCommand,IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
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


        var result = await repository.CreateAsync(book, cancellationToken);


        if (command.Tags != null)
        {
            await tagRepository.RemoveTagConnectionAsync(book.BookId, cancellationToken);
            for (int i = 0; i < command.Tags.Count; i++)
            {
                await tagRepository.AddTagConnectionAsync(book.BookId, command.Tags[i], cancellationToken);
            }
        }

        return result;
    }

}
