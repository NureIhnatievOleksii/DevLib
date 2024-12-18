﻿using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using DevLib.Domain.ReplyLinkAggregate;
using DevLib.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using DevLib.Domain.BookAggregate;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DevLibContext _context;

    public CommentRepository(DevLibContext context)
    {
        _context = context;
    }

    public async Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id, cancellationToken);
    }

    public async Task AddReviewAsync(Comment comment, CancellationToken cancellationToken)
    {
        comment.DateTime = DateTime.Now;
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Comment comment, CancellationToken cancellationToken)
    {
        _context.Comments.Update(comment);
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
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId, cancellationToken);
        if (comment == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Comment was not found." });
        }

        await DeleteRepliesAsync(commentId, cancellationToken);

        if (comment.ReplyId != null)
        {
            var replyLink = await _context.ReplyLinks.FirstOrDefaultAsync(r => r.ReplyId == comment.ReplyId, cancellationToken);
            if (replyLink != null)
            {
                _context.ReplyLinks.Remove(replyLink);
            }
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    private async Task DeleteRepliesAsync(Guid commentId, CancellationToken cancellationToken)
    {
        var replyLinks = await _context.ReplyLinks.Where(r => r.CommentId == commentId).ToListAsync(cancellationToken);

        foreach (var replyLink in replyLinks)
        {
            var replyComment = await _context.Comments.FirstOrDefaultAsync(c => c.ReplyId == replyLink.ReplyId, cancellationToken);
            if (replyComment != null)
            {
                await DeleteRepliesAsync(replyComment.CommentId, cancellationToken);

                _context.Comments.Remove(replyComment);
            }
            _context.ReplyLinks.Remove(replyLink);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<CommentDto> GetReplies(CommentDto comment, CancellationToken cancellationToken)
    {
        var replies = await _context.ReplyLinks.Where(r => r.CommentId == comment.CommentId).ToListAsync();

        foreach (var item in replies)
        {
            var current = await _context.Comments
                .Include(c => c.User) 
                .FirstOrDefaultAsync(c => c.ReplyId == item.ReplyId, cancellationToken);

            var commentDto = new CommentDto(
                AuthorName: current.User.UserName,
                AuthorImg: current.User.Photo,
                DateTime: current.DateTime,
                Text: current.Content,
                CommentId: current.CommentId,
                Comments: new List<CommentDto>(),
                UserId: current.UserId // Добавляем UserId
            );

            comment.Comments.Add(await GetReplies(commentDto, cancellationToken));
        }

        return comment;
    }


    private class CommentInfo
    {
        public string Title { get; set; }
        public Guid? PostId { get; set; }
        public Guid? BookId { get; set; }
    }

    private async Task<CommentInfo> GetTitle(Guid CommentId, CancellationToken cancellationToken)
    {
        Comment comment = await _context.Comments.FirstOrDefaultAsync(c =>c.CommentId == CommentId);

        if (comment.BookId != null)
        {
            var book = await _context.Books
                .Where(b => b.BookId == comment.BookId)
                .FirstOrDefaultAsync();
            return new CommentInfo() { BookId = book.BookId, Title =  book.BookName};
        }
        else if (comment.PostId != null)
        {
            var post = await _context.Posts
                .Where(p => p.PostId == comment.PostId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return new CommentInfo() { PostId = post.PostId, Title = post.Title };
        }

        Guid commentId = await _context.ReplyLinks.Where(r => r.ReplyId == comment.ReplyId).Select(r => r.CommentId).FirstOrDefaultAsync();

        return GetTitle(commentId, cancellationToken).Result;
    }

    public async Task<List<UserCommentDto>> GetUserCommentAsync(Guid UserId, CancellationToken cancellationToken = default)
    {
        var comments = await _context.Comments.Where(c => c.UserId == UserId).ToListAsync();

        List<UserCommentDto> result = new List<UserCommentDto>();


        foreach (var item in comments)
        {
            CommentInfo obj =  GetTitle(item.CommentId, cancellationToken).Result;
            result.Add(new UserCommentDto(obj.BookId, obj.PostId, obj.Title, item.CommentId, item.Content, item.DateTime));
        }

        return result;

    }

    public async Task<Comment> GetComment(Guid commentId, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId, cancellationToken);
        return comment;
    }
}