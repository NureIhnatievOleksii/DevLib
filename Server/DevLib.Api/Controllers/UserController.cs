using DevLib.Application.CQRS.Commands.Users.ResetUserPassword;
using DevLib.Application.CQRS.Commands.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        // todo ai add response toke after complete task with token configuration
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody, Required] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ResetUserPassword([FromBody, Required] ResetUserPasswordCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

    }

}
