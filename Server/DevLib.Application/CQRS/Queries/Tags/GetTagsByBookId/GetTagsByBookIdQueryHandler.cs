using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Tags.GetTagsByBookId;

public class GetTagsByBookIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetTagsByBookIdQuery, List<TagDto>>
{
    public async Task<List<TagDto>> Handle(
        GetTagsByBookIdQuery query, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetTagsByBookIdAsync(query.BookId,cancellationToken);
        
        return mapper.Map<List<TagDto>>(tags);
    }
}