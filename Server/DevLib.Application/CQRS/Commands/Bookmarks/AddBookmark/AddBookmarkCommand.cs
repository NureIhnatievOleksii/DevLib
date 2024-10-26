using MediatR;

namespace DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;

public record AddBookmarkCommand
(
    Guid UserId,
    Guid BookId
) : IRequest;
