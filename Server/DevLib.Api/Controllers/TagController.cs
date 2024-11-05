using DevLib.Application.CQRS.Commands.Tags.DeleteTagById;
using DevLib.Application.CQRS.Queries.Tags.GetTagsByBookId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        [HttpDelete("delete-teg/{tagId}")]
        public async Task<IActionResult> DeleteTagById(Guid tagId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteTagByIdCommand(tagId), cancellationToken);
            return Ok();
        }
    }

}
