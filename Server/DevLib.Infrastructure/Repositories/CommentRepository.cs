using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DevLibContext _context;

    public CommentRepository(DevLibContext context)
    {
        _context = context;
    }

    public async Task AddReviewAsync(Comment comment, CancellationToken cancellationToken)
    {
        comment.DateTime = DateTime.Now;
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task CreateAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
