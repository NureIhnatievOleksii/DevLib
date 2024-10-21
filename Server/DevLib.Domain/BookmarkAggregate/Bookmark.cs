using DevLib.Domain.BookAggregate;
using DevLib.Domain.UserAggregate;

namespace DevLib.Domain.BookmarkAggregate
{
    public class Bookmark
    {
        public Guid BookmarkId { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
