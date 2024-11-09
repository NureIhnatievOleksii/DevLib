﻿using DevLib.Application.CQRS.Commands.Posts.CreatePosts;
using DevLib.Application.CQRS.Queries.Posts.GetPostsById;
using DevLib.Application.CQRS.Queries.Posts.GetPosts;
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

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(Guid postId, CancellationToken cancellationToken)
        {
            try
            {
                var post = await mediator.Send(new GetPostByIdQuery(postId), cancellationToken);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(CancellationToken cancellationToken)
        {
            try
            {
                var posts = await mediator.Send(new GetPostsQuery(), cancellationToken);

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
