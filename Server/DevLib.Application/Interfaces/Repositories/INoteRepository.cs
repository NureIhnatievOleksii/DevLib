using DevLib.Domain.NotesAggregate;

namespace DevLib.Application.Interfaces.Repositories;

public interface INoteRepository
{
    Task AddNoteAsync(Note note, CancellationToken cancellationToken);
    Task<List<Note>> GetNotesByBookAndUserAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
    Task<Note?> GetByIdAsync(Guid noteId, CancellationToken cancellationToken);
    Task DeleteAsync(Note note, CancellationToken cancellationToken);

}
