using DevLib.Domain.UserAggregate;


namespace DevLib.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserWithPostsAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);

    }
}
