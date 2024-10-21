using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.PostAggregate;

namespace DevLib.Domain.DirectoryLinkAggregate
{
    public class DirectoryLink
    {
        public Guid DirectoryLinkId { get; set; }
        public Guid DirectoryId { get; set; }
        public Guid ArticleId { get; set; }
        public string ChapterName { get; set; }

        public DLDirectory Directory { get; set; }
        public Post Post { get; set; }

    }
}
