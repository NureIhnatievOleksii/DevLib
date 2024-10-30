using MediatR;

namespace DevLib.Application.CQRS.Queries.Tags.GetTagsByBookId
{
    public record GetTagsByBookIdQuery(Guid BookId) : IRequest<List<TagDto>>;
}
public record TagDto
{
    public Guid TagId { get; init; }
    public string TagText { get; init; }
}