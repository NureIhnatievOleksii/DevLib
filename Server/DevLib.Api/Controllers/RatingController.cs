using DevLib.Application.CQRS.Commands.Rating.AddRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RatingController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody, Required] AddRatingCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }

}
