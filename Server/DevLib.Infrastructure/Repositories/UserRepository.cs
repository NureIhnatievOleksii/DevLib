using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CommentAggregate;
using DevLib.Domain.UserAggregate;
using DevLib.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevLibContext _context;

        public async Task<User> GetUserWithPostsAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<List<Comment>> GetUserCommentsAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.Book)
                .ToListAsync(cancellationToken);
        }

    }
}
