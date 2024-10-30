using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Infrastructure.Database;

namespace DevLib.Infrastructure.PreloadingInformation
{
    public class SeedDataService
    {
        private readonly DevLibContext _context;

        public SeedDataService(DevLibContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (_context.Directories.Any() && _context.Articles.Any())
            {
                return;
            }

            var directories = new[]
            {
                new DLDirectory
                {
                    DirectoryId = Guid.NewGuid(),
                    DirectoryName = "C#",
                    ImgLink = "https://example.com/images/csharp.png"
                },
                new DLDirectory
                {
                    DirectoryId = Guid.NewGuid(),
                    DirectoryName = "Java",
                    ImgLink = "https://example.com/images/java.png"
                },
                new DLDirectory
                {
                    DirectoryId = Guid.NewGuid(),
                    DirectoryName = "Python",
                    ImgLink = "https://example.com/images/python.png"
                },
                new DLDirectory
                {
                    DirectoryId = Guid.NewGuid(),
                    DirectoryName = "JavaScript",
                    ImgLink = "https://example.com/images/javascript.png"
                }
            };

            _context.Directories.AddRange(directories);
            _context.SaveChanges();

            var articles = new[]
            {
                new Article
                {
                    ArticleId = Guid.NewGuid(),
                    DirectoryId = directories[0].DirectoryId,
                    Text = "C# is a modern object-oriented programming language...",
                    ChapterName = "Getting Started with C#"
                },
            };

            _context.Articles.AddRange(articles);
            _context.SaveChanges();
        }
    }
}
