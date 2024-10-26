using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.NotesAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.Repositories;

public class NoteRepository(DevLibContext context) : INoteRepository
{
    public async Task AddNoteAsync(Note note, CancellationToken cancellationToken)
    {
        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
