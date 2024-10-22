using DevLib.Domain.UserAggregate;
using DevLib.Domain.PostAggregate;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.ReplyLinkAggregate;

namespace DevLib.Domain.CommentAggregate
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        public Guid? UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public Guid? PostId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? ReplyId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
        public Book Book { get; set; }
        public ReplyLink Reply { get; set; }
    }
}