using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Dtos.Queries
{
    public record GetUserInfoQueryDto
(
    string Username,
    string Email,
    string Photo,
    List<UserPostDto> Posts,
    List<UserCommentDto> Comments
);
    public record UserPostDto
(
    Guid PostId,
    string Text
);

    public record UserCommentDto
    (
        Guid? BookId,
        Guid? PostId,
        string title,
        Guid CommentId,
        string Content,
        DateTime DateTime
    );
}
