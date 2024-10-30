using DevLib.Domain.BookAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.PostAggregate;

namespace DevLib.Domain.TagAggregate
{
    public class TagConnection
    {
        public Guid TagConnectionId { get; set; }
        public Guid TagId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? DirectoryId { get; set; }

        public Tag Tag { get; set; }
        public Post Post { get; set; }
        public Book Book { get; set; }
        public DLDirectory Directory { get; set; }
    }
}
