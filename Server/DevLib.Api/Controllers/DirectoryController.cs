using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
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
        public async Task<IActionResult> UpdateDirectory([FromBody, Required] UpdateDirectoryCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
