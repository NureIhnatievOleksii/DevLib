﻿using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
using DevLib.Application.CQRS.Queries.Articles.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]")]
    public class DirectoryController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-directory")] 
        public async Task<IActionResult> CreateDirectory([FromBody, Required] CreateDirectoryCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("edit-directory/{id}")]
        public async Task<IActionResult> UpdateDirectory(Guid id, [FromBody, Required] UpdateDirectoryCommand command, CancellationToken cancellationToken)
        {
            var updateCommand = command with { DirectoryId = id };
            await mediator.Send(updateCommand, cancellationToken);
            return Ok();
        }

        [HttpGet("get-article/{id}")]
        public async Task<IActionResult> GetArticleById(Guid id, CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetArticleByIdQuery(id), cancellationToken);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }
    }
}
