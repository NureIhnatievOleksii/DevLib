using DevLib.Domain.CommentAggregate;

namespace DevLib.Domain.ReplyLinkAggregate
{
    public class ReplyLink
    {
        public Guid ReplyId { get; set; }
        public Guid CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
