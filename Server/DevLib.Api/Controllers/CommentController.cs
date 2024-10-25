using DevLib.Application.CQRS.Commands.Comments.AddReview;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/comment")]
    public class CommentController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody, Required] AddReviewCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
