using DevLib.Application.CQRS.Commands.Admins.AssignAdminRole;
using DevLib.Application.CQRS.Commands.Admins.AssignModeratorRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevLib.Application.CQRS.Queries.Admins;
using DevLib.Application.CQRS.Commands.Admins.BanUser;

namespace DevLib.Api.Controllers
{
    [Route("api/admin")]
    public class AdminController(IMediator mediator) : ControllerBase
    {
        [HttpPost("assign-moderator-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignModeratorRole([FromBody] AssignModeratorRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken); 

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Moderator role assigned successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("assign-admin-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignAdminRole([FromBody] AssignAdminRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new { Message = "Admin role assigned successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery();
            var users = await mediator.Send(query, cancellationToken);

            return Ok(users);
        }
        [HttpPost("ban/{userId}")]
        public async Task<IActionResult> BanUser(Guid userId, [FromQuery] bool isBanned, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new BanUserCommand(userId, isBanned), cancellationToken);

            if (result)
            {
                return Ok(new { Message = isBanned ? "User banned successfully." : "User unbanned successfully." });
            }

            return NotFound(new { Message = "User not found." });
        }
    }
}
