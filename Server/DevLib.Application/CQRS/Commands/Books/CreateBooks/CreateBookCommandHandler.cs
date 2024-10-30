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

        // Определяем путь к папке для хранения книг
        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");
        if (!Directory.Exists(webRootPath))
        {
            Directory.CreateDirectory(webRootPath);
        }

        // Обработка изображения книги
        if (command.BookImg != null)
        {
            var imageFileName = Path.GetFileName(command.BookImg.FileName);
            var imageDestinationPath = Path.Combine(webRootPath, imageFileName);

            try
            {
                if (File.Exists(imageDestinationPath))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "This book image has already been added" });
                }

                using (var stream = new FileStream(imageDestinationPath, FileMode.Create))
                {
                    await command.BookImg.CopyToAsync(stream, cancellationToken);
                }

                // Обновляем путь к изображению в объекте Book
                book.BookImg = $"/Books/{imageFileName}";
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return IdentityResult.Failed(new IdentityError { Description = "Image file could not be copied" });
            }
        }

        // Обработка PDF-файла книги
        if (command.BookPdf != null)
        {
            var pdfFileName = Path.GetFileName(command.BookPdf.FileName);
            var pdfDestinationPath = Path.Combine(webRootPath, pdfFileName);

            try
            {
                if (File.Exists(pdfDestinationPath))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "This book PDF has already been added" });
                }

                using (var stream = new FileStream(pdfDestinationPath, FileMode.Create))
                {
                    await command.BookPdf.CopyToAsync(stream, cancellationToken);
                }

                // Обновляем путь к PDF в объекте Book
                book.FilePath = $"/Books/{pdfFileName}";
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return IdentityResult.Failed(new IdentityError { Description = "PDF file could not be copied" });
            }
        }

        // Сохраняем книгу в репозитории
        var result = await repository.CreateAsync(book, cancellationToken);

        // Добавляем связи с тегами
        for (int i = 0; i < command.Tags.Count; i++)
        {
            await tagRepository.AddTagConnectionAsync(book.BookId, command.Tags[i], cancellationToken);
        }

        return result;
    }

}
