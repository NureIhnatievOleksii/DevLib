using MediatR;

namespace DevLib.Application.CQRS.Commands.Notes.AddNote;

public record AddNoteCommand
(
    Guid UserId,
    Guid BookId,
    string Text
) : IRequest;
