using MediatR;

namespace DevLib.Application.CQRS.Queries.Notes
{
    public record GetNotesByBookAndUserQuery(Guid BookId, Guid UserId) : IRequest<List<GetNotesByBookAndUserQueryDto>>;
}
public class GetNotesByBookAndUserQueryDto
{
    public Guid NoteId { get; set; }
    public string Note { get; set; }
    public string Type { get; set; }
}
