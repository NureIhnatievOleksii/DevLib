using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Books.GetBookById;

public class GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
    : IRequestHandler<GetBookByIdQuery, GetBookByIdQueryDto>
{
    public async Task<GetBookByIdQueryDto> Handle(
        GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(query.Id, cancellationToken)
                       ?? throw new Exception("Book not found");

        return mapper.Map<GetBookByIdQueryDto>(book);
    }
}
