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
    public class GetBooksWithBookmarksQueryHandler : IRequestHandler<GetBooksWithBookmarksQuery, List<BookWithBookmarkDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookmarkRepository _bookmarkRepository;

        public GetBooksWithBookmarksQueryHandler(IBookRepository bookRepository, IBookmarkRepository bookmarkRepository)
        {
            _bookRepository = bookRepository;
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<List<BookWithBookmarkDto>> Handle(GetBooksWithBookmarksQuery query, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            var bookmarks = await _bookmarkRepository.GetBookmarksByUserIdAsync(query.UserId, cancellationToken);

            var bookmarkDict = bookmarks.ToDictionary(b => b.BookId, b => b.BookmarkId);

            return books.Select(book => new BookWithBookmarkDto
            {
                BookId = book.BookId,
                BookName = book.BookName,
                Author = book.Author,
                FilePath = book.FilePath,
                BookImg = book.BookImg,
                PublicationDateTime = book.PublicationDateTime,
                BookmarkId = bookmarkDict.ContainsKey(book.BookId) ? bookmarkDict[book.BookId] : null
            }).ToList();
        }
    }

}
