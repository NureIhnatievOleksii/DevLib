using AutoMapper;
using DevLib.Application.CQRS.Commands.Users.ResetUserPassword;
using DevLib.Application.CQRS.Commands.Users.UpdateUser;
using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.CQRS.Queries.User;
using DevLib.Domain.UserAggregate;
using EmailService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.Differencing;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/user")]
    public class UserController(IMediator mediator, IEmailSender emailSender, UserManager<User> userManager) : ControllerBase
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

        [HttpGet("{userId}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetUserInfo(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserInfoQuery(userId);
            var userInfo = await mediator.Send(query, cancellationToken);

            return Ok(userInfo);
        }

        [HttpPost("forgot-password")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(forgotPassword.Email!);
            if (user is null)
                return BadRequest("User not found");

            var token = userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                { "token", await token },
                { "email", forgotPassword.Email! }
            };

            var callback = QueryHelpers.AddQueryString(forgotPassword.ClientUri!, param);

            var message = new Message([user.Email], "Reset Password", callback);

            await emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("reset-user-password")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> ResetUserPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(resetPassword.Email!);
            if (user is null)
                return BadRequest("User not found");

            var result = await userManager.ResetPasswordAsync(user, resetPassword.Token!, resetPassword.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }
    }
}
