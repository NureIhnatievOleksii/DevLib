using MediatR;

namespace DevLib.Application.CQRS.Commands.Bookmarks.DeleteBookmarkById;

public class DeleteBookmarkByIdCommand : IRequest<bool>
{
    public Guid BookmarkId { get; set; }

    public DeleteBookmarkByIdCommand(Guid bookmarkId)
    {
        BookmarkId = bookmarkId;
    }
}