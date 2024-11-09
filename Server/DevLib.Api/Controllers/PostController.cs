﻿using DevLib.Application.CQRS.Commands.Posts.CreatePosts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/post")]
    public class PostController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody, Required] CreatePostCommand command, CancellationToken cancellationToken)
        {
            try
            {
                string post = await mediator.Send(command, cancellationToken);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
