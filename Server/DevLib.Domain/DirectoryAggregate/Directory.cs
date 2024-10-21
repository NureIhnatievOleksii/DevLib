using DevLib.Domain.PostAggregate;
using DevLib.Domain.DirectoryLinkAggregate;

namespace DevLib.Domain.DirectoryAggregate
{
    public class DLDirectory
    {
        public Guid DirectoryId { get; set; }
        public string DirectoryName { get; set; }

        public ICollection<DirectoryLink> DirectoryLinks { get; set; } = new List<DirectoryLink>();
    }
}
