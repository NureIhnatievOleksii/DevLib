using DevLib.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Bookmarks.DeleteBookmarkById
{
    public class DeleteBookmarkByIdCommandHandler : IRequestHandler<DeleteBookmarkByIdCommand, bool>
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public DeleteBookmarkByIdCommandHandler(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<bool> Handle(DeleteBookmarkByIdCommand request, CancellationToken cancellationToken)
        {
            var bookmark = await _bookmarkRepository.GetByIdAsync(request.BookmarkId, cancellationToken);
            if (bookmark == null)
            {
                return false;
            }

            await _bookmarkRepository.DeleteAsync(bookmark, cancellationToken);
            return true;
        }
    }
}
