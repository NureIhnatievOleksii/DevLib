namespace DevLib.Application.CQRS.Dtos.Queries;

public record GetArticleByIdQueryDto
(
    Guid ArticleId,
    Guid DirectoryId,
    string Text,
    string ChapterName
);
