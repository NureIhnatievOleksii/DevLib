using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.NotesAggregate;
using DevLib.Domain.TagAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DevLibContext _context;

        public TagRepository(DevLibContext context)
        {
            _context = context;
        }
        public async Task AddTagAsync(Tag tag, CancellationToken cancellationToken)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Tag>> GetTagsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var tagIds = await _context.TagConnections
                .Where(tc => tc.BookId == bookId)
                .Select(tc => tc.TagId)
                .ToListAsync(cancellationToken);

            if (!tagIds.Any())
            {
                return new List<Tag>();
            }

            var tags = await _context.Tags
                .Where(t => tagIds.Contains(t.TagId))
                .ToListAsync(cancellationToken);

            return tags;
        }

        public async Task AddTagConnectionAsync(Guid bookId, string tag, CancellationToken cancellationToken)
        {
            var existingConnections = await _context.TagConnections
                .Where(tc => tc.BookId == bookId)
                .ToListAsync(cancellationToken);

            if (existingConnections.Any())
            {
                _context.TagConnections.RemoveRange(existingConnections);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var currentTag = await _context.Tags.FirstOrDefaultAsync(c => c.TagText == tag, cancellationToken);
            Guid tagId;

            if (currentTag == null)
            {
                var newTag = new Tag
                {
                    TagId = Guid.NewGuid(),
                    TagText = tag
                };

                await AddTagAsync(newTag, cancellationToken);
                tagId = newTag.TagId;
            }
            else
            {
                tagId = currentTag.TagId;
            }

            var tagConnection = new TagConnection
            {
                TagId = tagId,
                BookId = bookId,
                TagConnectionId = Guid.NewGuid()
            };

            await _context.TagConnections.AddAsync(tagConnection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }



    }
}
