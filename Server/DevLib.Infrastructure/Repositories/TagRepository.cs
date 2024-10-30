using DevLib.Application.Interfaces.Repositories;
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

        public async Task AddTagConnectionAsync(Guid bookId, Guid tagId, CancellationToken cancellationToken)
        {
            var tagConnection = new TagConnection
            {
                TagId = tagId,
                BookId = bookId,
                TagConnectionId = Guid.NewGuid()
            };

            await _context.TagConnections.AddAsync(tagConnection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddTagConnectionToDirectoryAsync(Guid directoryId, Guid tagId, CancellationToken cancellationToken)
        {
            var tagConnection = new TagConnection
            {
                TagId = tagId,
                DirectoryId = directoryId, // Присвоение идентификатора каталога
                TagConnectionId = Guid.NewGuid()
            };

            await _context.TagConnections.AddAsync(tagConnection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Другие методы...
    }
}
