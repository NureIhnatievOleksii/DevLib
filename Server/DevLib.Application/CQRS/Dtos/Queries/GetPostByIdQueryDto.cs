namespace DevLib.Application.CQRS.Dtos.Queries;

public record CommentDto
(
    string AuthorName,
    Guid CommentId,
    string? AuthorImg,
    DateTime DateTime,
    string Text,
    List<CommentDto> Comments 
);

public record GetPostByIdQueryDto
(
    string PostName,
    string Text,
    DateTime DateTime,
    string AuthorName,
    string? AuthorImg,
    int CommentsQuantity,
    Guid PostId,
    List<CommentDto> Comments 
);

