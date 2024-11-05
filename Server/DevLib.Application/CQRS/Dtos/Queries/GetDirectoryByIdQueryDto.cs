namespace DevLib.Application.CQRS.Dtos.Queries
{
    public record GetDirectoryDto
    (
        Guid DirectoryId,
        string DirectoryName,
        string ImgLink,
        IEnumerable<ArticleDto> Articles
    );

    public record ArticleDto
    (
        Guid ArticleId,
        string Name
    );
}
