namespace DevLib.Application.CQRS.Dtos.Queries;

public record GetPostsQueryDto
(
    string PostName,
    DateTime DateTime,
    string AuthorName,
    string? AuthorImg,
    Guid PostId,
    int CommentsQuantity
);

