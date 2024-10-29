using DevLib.Application.CQRS.Commands.Notes.AddNote;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/note")]
    public class NoteController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-note")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> Add_Note([FromBody, Required] AddNoteCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }

}
