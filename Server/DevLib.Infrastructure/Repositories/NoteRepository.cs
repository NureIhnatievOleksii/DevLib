using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.NotesAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DevLib.Infrastructure.Repositories;

public class NoteRepository(DevLibContext context) : INoteRepository
{
    public async Task AddNoteAsync(Note note, CancellationToken cancellationToken)
    {
        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Note>> GetNotesByBookAndUserAsync(Guid bookId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.Notes
            .Where(n => n.BookId == bookId && n.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    public async Task<Note?> GetByIdAsync(Guid noteId, CancellationToken cancellationToken)
    {
        return await context.Notes.FirstOrDefaultAsync(c => c.NoteId == noteId, cancellationToken);
    }

    public async Task DeleteAsync(Note note, CancellationToken cancellationToken)
    {
        context.Notes.Remove(note);
        await context.SaveChangesAsync(cancellationToken);
    }
}
