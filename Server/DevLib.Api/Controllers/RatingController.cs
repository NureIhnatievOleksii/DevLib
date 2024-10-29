using DevLib.Application.CQRS.Commands.Rating.AddRating;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/raiting")]
    public class RatingController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-rating")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Add_Rating([FromBody, Required] AddRatingCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }

}
