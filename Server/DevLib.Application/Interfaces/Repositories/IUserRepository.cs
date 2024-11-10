using DevLib.Domain.CommentAggregate;
using DevLib.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserWithPostsAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<Comment>> GetUserCommentsAsync(Guid userId, CancellationToken cancellationToken);
    }
}
