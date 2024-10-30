using AutoMapper;
using DevLib.Application.CQRS.Queries.Books.SearchBooks;
using DevLib.Application.Interfaces.Repositories;
using MediatR;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, List<BookNameDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public SearchBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<List<BookNameDto>> Handle(SearchBooksQuery query, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        var filteredBooks = string.IsNullOrWhiteSpace(query.BookName)
            ? books
            : books.Where(d => d.BookName.Contains(query.BookName, StringComparison.OrdinalIgnoreCase)).ToList();

        return _mapper.Map<List<BookNameDto>>(filteredBooks);
    }

}
