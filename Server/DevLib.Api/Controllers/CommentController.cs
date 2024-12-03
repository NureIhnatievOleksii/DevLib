using DevLib.Application.CQRS.Commands.Bookmarks.DeleteBookmarkById;
using DevLib.Application.CQRS.Commands.Comments;
using DevLib.Application.CQRS.Commands.Comments.AddReview;
using DevLib.Application.CQRS.Commands.Comments.DeleteCommentById;
using DevLib.Application.CQRS.Commands.Comments.UpdateComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/comment")]
    public class CommentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-review")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> AddReview([FromBody, Required] AddReviewCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> CreateComment([FromBody, Required] CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var commentId = await mediator.Send(command, cancellationToken);
            return Ok(new { commentId });
        }

        [HttpPost("reply")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> CreateReply([FromBody, Required] CreateReplyCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Reply was succesfully added" });
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{commentId:guid}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> DeleteComment([ Required] Guid commentId, CancellationToken cancellationToken)
        {
            var command = new DeleteCommentByIdCommand(commentId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Comment was succesfully deleted" });
            }

            return BadRequest(result.Errors);

        }

        [HttpPut("update-coments")]
        [Authorize(Roles = "Client,Admin")]

        public async Task<IActionResult> UpdateComment([FromBody, Required] UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest("Incorrect data entry");
            }

            object result = await mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return BadRequest("There is no such comment");
            }

            return Ok(result);
        }

    }
}
