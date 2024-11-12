using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.BookmarkAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;

public class AddBookmarkCommandHandler(IBookmarkRepository repository, IMapper mapper)
    : IRequestHandler<AddBookmarkCommand>
{
    public async Task Handle(AddBookmarkCommand command, CancellationToken cancellationToken)
    {
        var existingBookmarks = await repository.GetBookmarksByUserIdAsync(command.UserId, cancellationToken);
        var existingBookmark = existingBookmarks.FirstOrDefault(b => b.BookId == command.BookId);

        if (existingBookmark != null)
        {
            await repository.DeleteAsync(existingBookmark, cancellationToken);
        }
        else
        {
            var bookmark = mapper.Map<Bookmark>(command);
            await repository.AddBookmarkAsync(bookmark, cancellationToken);
        }
    }
}
