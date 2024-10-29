using DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;
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
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Add_Bookmark([FromBody, Required] AddBookmarkCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }

}
