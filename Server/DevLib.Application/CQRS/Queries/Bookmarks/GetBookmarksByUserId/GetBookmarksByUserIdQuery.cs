using MediatR;

namespace DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId
{
    public class GetBooksWithBookmarksQuery : IRequest<List<BookWithBookmarkDto>>
    {
        public Guid UserId { get; }

        public GetBooksWithBookmarksQuery(Guid userId)
        {
            UserId = userId;
        }
    }
    public class BookWithBookmarkDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
        public string BookImg { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public Guid? BookmarkId { get; set; } 
    }
}
