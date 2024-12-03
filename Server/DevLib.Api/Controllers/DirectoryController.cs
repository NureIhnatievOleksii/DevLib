using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.CreateArticles;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateArticles;
using DevLib.Application.CQRS.Commands.Directories.DeleteArticles;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.CQRS.Queries.Articles.GetAllArticlesNamesByDirectoryId;
using DevLib.Application.CQRS.Queries.Articles.GetArticleById;
using DevLib.Application.CQRS.Queries.Directories.GetDirectoryById;
using DevLib.Application.CQRS.Queries.Directories.SearchDirectories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using DevLib.Application.CQRS.Queries.Directories;
using DevLib.Application.CQRS.Commands.Directories.DeleteDirectory;

namespace DevLib.Api.Controllers
{
    [Route("api/directory")]
    public class DirectoryController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-directory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDirectory([FromForm, Required] CreateDirectoryCommand command, CancellationToken cancellationToken)
        {
            string directory = await mediator.Send(command, cancellationToken);
            return Ok(directory);
        }

        [HttpPost("add-article")]
        public async Task<IActionResult> CreateArticle([FromBody, Required] CreateArticleCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }


        [HttpPut("edit-directory")]
        public async Task<IActionResult> UpdateDirectory([FromForm, Required] UpdateDirectoryCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("edit-article")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateArticle([FromForm, Required] UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("get-article/{articleId}")]
        public async Task<IActionResult> GetArticleById(Guid articleId, CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetArticleByIdQuery(articleId), cancellationToken);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpGet("get-all-chapter-name/{directoryId}")]
        public async Task<ActionResult<List<GetAllArticlesNamesByDirectoryIdDto>>> GetArticleNames(Guid directoryId, CancellationToken cancellationToken)
        {
            var articlesNames = await mediator.Send(new GetArticleNamesByDirectoryIdQuery(directoryId), cancellationToken);
            return Ok(articlesNames);
        }

        [HttpGet("get-directory/{directoryId}")]
        public async Task<IActionResult> GetDirectoryById(Guid directoryId, CancellationToken cancellationToken)
        {
            var directoryDto = await mediator.Send(new GetDirectoryByIdQuery(directoryId), cancellationToken);

            return Ok(directoryDto);
        }

        [HttpGet("search-directories")]
        public async Task<IActionResult> SearchDirectories(CancellationToken cancellationToken = default, string? directoryName = null)
        {
            var directories = await mediator.Send(new SearchDirectoriesQuery(directoryName), cancellationToken);
            return Ok(directories);
        }


        [HttpGet("get-last-directory")]
        public async Task<IActionResult> GetLastDirectories(CancellationToken cancellationToken)
        {
            var directories = await mediator.Send(new LastDirectoriesQuery(), cancellationToken);
            return Ok(directories);
        }

        [HttpDelete("delete-article/{articleId}")]
        public async Task<IActionResult> DeleteArticle(Guid articleId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteArticleCommand(articleId), cancellationToken);
            return Ok();
        }

        [HttpDelete("{idDirectory}")]
        public async Task<IActionResult> DeleteDirectory(Guid idDirectory, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteDirectoryCommand(idDirectory), cancellationToken);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = result.Errors });
            }

            return Ok();
        }
    }
}