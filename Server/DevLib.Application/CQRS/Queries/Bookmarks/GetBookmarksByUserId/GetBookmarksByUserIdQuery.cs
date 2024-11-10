using MediatR;

namespace DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId
{
    public class GetBookmarksByUserIdQuery : IRequest<List<Guid>>
    {
        public Guid UserId { get; }

        public GetBookmarksByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
