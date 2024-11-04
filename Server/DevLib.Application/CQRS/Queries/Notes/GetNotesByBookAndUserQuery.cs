using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Queries.Notes
{
    public record GetNotesByBookAndUserQuery(Guid BookId, Guid UserId) : IRequest<List<GetNotesByBookAndUserQueryDto>>;
}
public class GetNotesByBookAndUserQueryDto
{
    public Guid NoteId { get; set; }
    public string Note { get; set; }
}