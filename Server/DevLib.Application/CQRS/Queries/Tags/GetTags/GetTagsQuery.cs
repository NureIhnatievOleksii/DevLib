using DevLib.Domain.TagAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Tags.GetTags
{
    public record GetTagsQuery : IRequest<List<Tag>>;
}