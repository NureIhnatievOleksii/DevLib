using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.TagAggregate;

namespace DevLib.Application.CQRS.Queries.Tags.GetTags;

public class GetTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<GetTagsQuery, List<Tag>>
{
    public async Task<List<Tag>> Handle(
        GetTagsQuery query, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetTagsAsync(cancellationToken);
        
        return mapper.Map<List<Tag>>(tags);
    }
}