using AutoMapper;
using DevLib.Application.CQRS.Queries.Books.SearchBooks;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using MediatR;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, List<BookNameDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public SearchBooksQueryHandler(IBookRepository bookRepository, ITagRepository tagRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<List<BookNameDto>> Handle(SearchBooksQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var books = new List<Book>(await _bookRepository.GetAllAsync(cancellationToken));

            List<Book> filteredBooks = books;

            if (!string.IsNullOrWhiteSpace(query.tag))
            {
                var tagConnections = await _tagRepository.GetTagConnectionByText(query.tag, cancellationToken);
                filteredBooks = filteredBooks
                    .Where(book => tagConnections.Any(tag => tag.BookId == book.BookId))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(query.BookName))
            {
                filteredBooks = filteredBooks
                    .Where(book => book.BookName.Contains(query.BookName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return _mapper.Map<List<BookNameDto>>(filteredBooks);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while searching books.", ex);
        }
    }

}
