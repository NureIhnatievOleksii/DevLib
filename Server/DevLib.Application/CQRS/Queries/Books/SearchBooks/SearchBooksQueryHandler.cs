using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.SearchBooks
{
    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public SearchBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(SearchBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            var filteredBooks = books
                .Where(d => d.BookName.Contains(query.BookName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.Map<List<BookDto>>(filteredBooks);
        }
    }
}
