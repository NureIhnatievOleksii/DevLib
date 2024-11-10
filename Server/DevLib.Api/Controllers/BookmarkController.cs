using DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;
using DevLib.Application.CQRS.Queries.Bookmarks.GetBookmarksByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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
        public async Task<IActionResult> GetBookmarksByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetBookmarksByUserIdQuery(userId);
            var bookIds = await mediator.Send(query, cancellationToken);
            return Ok(bookIds);
        }
    }
}