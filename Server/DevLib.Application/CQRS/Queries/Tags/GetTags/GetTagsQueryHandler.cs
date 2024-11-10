using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Tags.GetTags;

public class GetTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetTagsQuery, List<TagDto>>
{
    public async Task<List<TagDto>> Handle(
        GetTagsQuery query, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetTagsAsync(cancellationToken);
        
        return mapper.Map<List<TagDto>>(tags);
    }
}