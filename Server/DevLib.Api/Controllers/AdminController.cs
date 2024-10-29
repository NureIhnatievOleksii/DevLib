﻿using DevLib.Application.CQRS.Commands.Admins.AssignAdminRole;
using DevLib.Application.CQRS.Commands.Admins.AssignModeratorRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DevLib.Api.Controllers
{
    [Route("api/auth")]
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
    }
}
