using DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId;
using DevLib.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId
{
    public class GetBookmarksByUserIdQueryHandler : IRequestHandler<GetBookmarksByUserIdQuery, List<Guid>>
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public GetBookmarksByUserIdQueryHandler(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<List<Guid>> Handle(GetBookmarksByUserIdQuery query, CancellationToken cancellationToken)
        {
            var bookmarks = await _bookmarkRepository.GetBookmarksByUserIdAsync(query.UserId, cancellationToken);
            return bookmarks.Select(b => b.BookId).ToList();
        }
    }
}
