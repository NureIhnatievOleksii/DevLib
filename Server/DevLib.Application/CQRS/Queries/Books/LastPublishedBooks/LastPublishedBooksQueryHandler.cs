using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.CQRS.Queries.Books.LastPublishedBooks;
using MediatR;

public class LastPublishedBooksQueryHandler : IRequestHandler<LastPublishedBooksQuery, List<LastPublishedBookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public LastPublishedBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<List<LastPublishedBookDto>> Handle(LastPublishedBooksQuery query, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        var lastPublishedBooks = books
            .OrderByDescending(d => d.PublicationDateTime)
            .Take(10)
            .ToList();

        return _mapper.Map<List<LastPublishedBookDto>>(lastPublishedBooks);
    }
}
