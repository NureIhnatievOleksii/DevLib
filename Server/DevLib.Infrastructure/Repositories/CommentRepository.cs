using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.ReplyLinkAggregate;

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

    public async Task<IdentityResult> CreateReply(Comment comment,Guid CommentId, CancellationToken cancellationToken)
    {
        ReplyLink replyLink = new ReplyLink { CommentId = CommentId, ReplyId = Guid.NewGuid() };

        await _context.ReplyLinks.AddAsync(replyLink, cancellationToken);

        comment.ReplyId = replyLink.ReplyId;
        comment.Reply = replyLink;

        await _context.Comments.AddAsync(comment, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }
}
