using AutoMapper;
using DevLib.Application.CQRS.Commands.Users.ResetUserPassword;
using DevLib.Application.CQRS.Commands.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
            [HttpPut("edit-profile")]
            [Authorize(Roles = "Client")]
            public async Task<IActionResult> UpdateUser([FromBody, Required] UpdateUserCommand command, CancellationToken cancellationToken)
            {
                var result = await mediator.Send(command, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(new { result.Token, Message = "User profile updated successfully." });
                }

                return BadRequest(result.ErrorMessage);
            }

        [HttpPost("reset-user-password")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ResetUserPassword([FromBody, Required] ResetUserPasswordCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

    }

}
