using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.CQRS.Queries.Articles.GetAllArticlesNamesByDirectoryId;
using DevLib.Application.CQRS.Queries.Articles.GetArticleById;
using DevLib.Application.CQRS.Queries.Directories.GetDirectoryById;
using DevLib.Application.CQRS.Queries.Directories.SearchDirectories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/directory")]
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

        [HttpGet("get-all-chapter-name/{id}")]
        public async Task<ActionResult<List<GetAllArticlesNamesByDirectoryIdDto>>> GetArticleNames(Guid id, CancellationToken cancellationToken)
        {
            var articlesNames = await mediator.Send(new GetArticleNamesByDirectoryIdQuery(id), cancellationToken);
            return Ok(articlesNames);
        }

        [HttpGet("get-directory/{directoryId}")]
        public async Task<IActionResult> GetDirectoryById(Guid directoryId, CancellationToken cancellationToken)
        {
            var directoryDto = await mediator.Send(new GetDirectoryByIdQuery(directoryId), cancellationToken);

            return Ok(directoryDto);
        }

        [HttpGet("search-directories/{directoryName}")]
        public async Task<IActionResult> SearchDirectories(string directoryName, CancellationToken cancellationToken)
        {
            var directories = await mediator.Send(new SearchDirectoriesQuery(directoryName), cancellationToken);
            return Ok(directories);
        }
    }
}
