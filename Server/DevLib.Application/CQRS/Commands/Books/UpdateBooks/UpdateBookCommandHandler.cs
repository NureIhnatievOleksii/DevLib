using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository bookRepository;
    //private readonly ITagRepository tagRepository;
    private readonly IMapper mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository/*, ITagRepository tagRepository*/, IMapper mapper)
    {
        this.bookRepository = bookRepository;
        //this.tagRepository = tagRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(command.BookId, cancellationToken)
                       ?? throw new Exception("Book not found");

        book.BookName = command.BookName;
        book.Author = command.Author;
        book.PublicationDateTime = command.PublicationDateTime;

        if (command.BookImg != null)
        {
            var filePath = Path.Combine("Uploads", command.BookImg.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.BookImg.CopyToAsync(stream, cancellationToken);
            }
            // todo update database data
        }

        if (command.BookPdf != null)
        {
            var pdfPath = Path.Combine("Uploads", command.BookPdf.FileName);
            using (var stream = new FileStream(pdfPath, FileMode.Create))
            {
                await command.BookPdf.CopyToAsync(stream, cancellationToken);
            }
            // todo update database data
        }

        // todo make loading the tags
        //var existingTags = book.Tags.Select(t => t.TagId).ToHashSet();

        //foreach (var tagDto in command.Tags)
        //{
        //    if (tagDto.TagId.HasValue && existingTags.Contains(tagDto.TagId.Value))
        //    {
        //        var tag = await tagRepository.GetByIdAsync(tagDto.TagId.Value, cancellationToken);
        //        tag.TagName = tagDto.TagText;
        //        await tagRepository.UpdateAsync(tag, cancellationToken);
        //    }
        //    else
        //    {
        //        var newTag = new Tag { TagName = tagDto.TagText };
        //        await tagRepository.CreateAsync(newTag, cancellationToken);
        //        book.Tags.Add(newTag);
        //    }
        //}

        mapper.Map(command, book);

        await bookRepository.UpdateAsync(book, cancellationToken);
    }
}
