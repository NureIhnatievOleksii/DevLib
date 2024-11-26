using DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;
using DevLib.Application.CQRS.Commands.Bookmarks.DeleteBookmarkById;
using DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/bookmark")]
    public class BookmarkController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-bookmark")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> Add_Bookmark([FromBody, Required] AddBookmarkCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
        [HttpGet("{userId:guid}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetBooksWithBookmarks(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetBooksWithBookmarksQuery(userId);
            var booksWithBookmarks = await mediator.Send(query, cancellationToken);
            return Ok(booksWithBookmarks);
        }
        [HttpDelete("{bookmarkId:guid}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> DeleteBookmarkById(Guid bookmarkId, CancellationToken cancellationToken)
        {
            var command = new DeleteBookmarkByIdCommand(bookmarkId);
            var result = await mediator.Send(command, cancellationToken);

            if (result)
            {
                return Ok(new { Message = "Bookmark deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Bookmark not found" });
            }
        }
    }
}