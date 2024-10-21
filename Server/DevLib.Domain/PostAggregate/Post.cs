using DevLib.Domain.UserAggregate;

namespace DevLib.Domain.PostAggregate
{
    public class Post
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public bool IsArticle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public User User { get; set; }
    }
}