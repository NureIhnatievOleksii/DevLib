using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using MediatR;
using DevLib.Domain.NotesAggregate;

namespace DevLib.Application.CQRS.Commands.Notes.AddNote;

public class AddNoteCommandHandler(INoteRepository repository, IMapper mapper)
    : IRequestHandler<AddNoteCommand>
{
    public async Task Handle(AddNoteCommand command, CancellationToken cancellationToken)
    {
        var note = mapper.Map<Note>(command);

        note.Type = command.Type;

        await repository.AddNoteAsync(note, cancellationToken);
    }
}
