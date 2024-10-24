using DevLib.Domain.DirectoryAggregate;

namespace DevLib.Domain.ArticleAggregate
{
    public class Article
    {
        public Guid ArticleId { get; set; }
        public Guid DirectoryId { get; set; }
        public string Text { get; set; }
        public string ChapterName { get; set; }

        public DLDirectory Directory { get; set; }
    }
}