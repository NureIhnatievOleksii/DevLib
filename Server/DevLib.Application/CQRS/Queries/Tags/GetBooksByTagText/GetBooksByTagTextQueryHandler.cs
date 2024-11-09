using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;

namespace DevLib.Application.CQRS.Queries.Tags.GetBooksByTagText;

public class GetTagsByTagTextQueryHandler(ITagRepository tagRepository, IBookRepository bookRepository,IMapper mapper)
    : IRequestHandler<GetBooksByTagTextQuery, List<BookNameDto>>
{
    public async Task<List<BookNameDto>> Handle(
        GetBooksByTagTextQuery query, CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAllAsync(cancellationToken);

        var tagConnections = await tagRepository.GetTagConnectionByText(query.tagText, cancellationToken);

        List<Book> filteredBooks = new List<Book>();

        foreach (var book in books) 
        {
            foreach (var tag in tagConnections) 
            { 
                if(tag.BookId == book.BookId)
                {
                    filteredBooks.Add(book);
                }
            }
        }

        return mapper.Map<List<BookNameDto>>(filteredBooks);
    }
}