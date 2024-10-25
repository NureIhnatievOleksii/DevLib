using DevLib.Application.CQRS.Commands.Comments.AddReview;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CommentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add_Review([FromBody, Required] AddReviewCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }

}
