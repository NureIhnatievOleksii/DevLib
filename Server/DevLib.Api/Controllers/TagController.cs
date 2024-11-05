using DevLib.Application.CQRS.Commands.Tags.UpdateTags;
using DevLib.Application.CQRS.Queries.Tags.GetTagsByBookId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/tag")]
    public class TagController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-tags/{bookId}")]
        public async Task<IActionResult> GetTagsById(Guid bookId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetTagsByBookIdQuery(bookId), cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("edit-tag")]
        public async Task<IActionResult> UpdateDirectory([FromBody, Required] UpdateTagCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }

}
