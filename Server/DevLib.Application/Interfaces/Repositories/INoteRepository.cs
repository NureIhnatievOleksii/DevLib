using DevLib.Domain.NotesAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface INoteRepository
{
    Task AddNoteAsync(Note note, CancellationToken cancellationToken);
    Task<List<Note>> GetNotesByBookAndUserAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
}
