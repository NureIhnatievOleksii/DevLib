using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.ReplyLinkAggregate;
using DevLib.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IdentityResult> CreateReply(Comment comment, Guid CommentId, CancellationToken cancellationToken)
    {
        try
        {
            ReplyLink replyLink = new ReplyLink { CommentId = CommentId, ReplyId = Guid.NewGuid() };

            await _context.ReplyLinks.AddAsync(replyLink, cancellationToken);

            comment.ReplyId = replyLink.ReplyId;
            comment.Reply = replyLink;

            await _context.Comments.AddAsync(comment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.ToString() });
        }
        return IdentityResult.Success;
    }


    public async Task<IdentityResult> DeleteCommentAsync(Guid commentId, CancellationToken cancellationToken = default)
    {
        Comment comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId, cancellationToken);
        if(comment == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Comment was not found." });
        }

        var replies = await _context.ReplyLinks.Where(r=> r.CommentId == commentId).ToListAsync();

        foreach (var replyLink in replies) {
            Comment replyComment = await _context.Comments.FirstOrDefaultAsync(c => c.ReplyId == replyLink.ReplyId, cancellationToken);
            if (replyComment != null)
            {
                replyComment.Reply = null;
                replyComment.ReplyId = null;
            }
            _context.ReplyLinks.Remove(replyLink);
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (comment.ReplyId != null)
        {
            var reply = await _context.ReplyLinks.FirstOrDefaultAsync(r => r.ReplyId == comment.ReplyId,cancellationToken);
            _context.ReplyLinks.Remove(reply);
        }
        _context.Comments.Remove(comment);

        await _context.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }
}