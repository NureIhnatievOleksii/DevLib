using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using Microsoft.AspNetCore.Identity;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using System.IO;
using DevLib.Domain.TagAggregate;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository repository, ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<UpdateBookCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(command.BookId, cancellationToken);

        string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Books");
        if (!Directory.Exists(webRootPath))
        {
            Directory.CreateDirectory(webRootPath);
        }
        if (!string.IsNullOrWhiteSpace(command.BookName))
        {
            book.BookName = command.BookName;
        }

        if (!string.IsNullOrWhiteSpace(command.Author))
        {
            book.Author = command.Author;
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

        if (command.TagId != null)
        {
            try
            {
                Guid tagId = (Guid)command.TagId;

                var tags = await tagRepository.GetTagsByBookIdAsync(command.BookId, cancellationToken);

                if (tags != null)
                {
                    foreach (var existingTag in tags)
                    {
                        await tagRepository.RemoveTagConnectionAsync(command.BookId, existingTag.TagId, cancellationToken);
                    }
                }

                var tagToAdd = await tagRepository.GetTagByIdAsync(tagId, cancellationToken);
                await tagRepository.AddTagConnectionAsync(command.BookId, null, tagToAdd.TagText, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the book tags.", ex);
            }
        }
        var result = await repository.UpdateAsync(book, cancellationToken);

        return result;
    }
}
