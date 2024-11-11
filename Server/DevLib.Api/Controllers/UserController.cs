using AutoMapper;
using DevLib.Application.CQRS.Commands.Users.ResetUserPassword;
using DevLib.Application.CQRS.Commands.Users.UpdateUser;
using DevLib.Application.CQRS.Queries.User;
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
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> UpdateUser([FromForm, Required] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new { result.Token, Message = "User profile updated successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }


        [HttpPost("reset-user-password")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> ResetUserPassword([FromBody, Required] ResetUserPasswordCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetUserInfo(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserInfoQuery(userId);
            var userInfo = await mediator.Send(query, cancellationToken);

            return Ok(userInfo);
        }

    }

}
