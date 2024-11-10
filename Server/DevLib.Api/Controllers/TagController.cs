using DevLib.Application.CQRS.Commands.Tags.UpdateTags;
using DevLib.Application.CQRS.Commands.Tags.CreateTags;
using DevLib.Application.CQRS.Queries.Tags.GetTagsByBookId;
using DevLib.Application.CQRS.Queries.Tags.GetTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DevLib.Application.CQRS.Commands.Tags.DeleteTagById;
using DevLib.Application.CQRS.Queries.Tags.GetBooksByTagText;

namespace DevLib.Api.Controllers
{
    [Route("api/tag")]
    public class TagController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-tag")]
        public async Task<IActionResult> CreateTag([FromBody, Required] CreateTagCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("edit-tag")]
        public async Task<IActionResult> UpdateTag([FromBody, Required] UpdateTagCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("get-tags")]
        public async Task<IActionResult> GetTags(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetTagsQuery(),cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

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

        [HttpGet("{tagText}/books")]
        public async Task<IActionResult> GetBooksByTagText(string tagText, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetBooksByTagTextQuery(tagText), cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("/{tagId}")]
        public async Task<IActionResult> DeleteTag(Guid tagId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteTagByIdCommand(tagId), cancellationToken);
            return Ok();
        }

    }
}
