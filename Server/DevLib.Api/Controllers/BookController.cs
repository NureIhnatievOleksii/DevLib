using DevLib.Application.CQRS.Commands.Books.CreateBooks;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using DevLib.Application.CQRS.Queries.Books.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BookController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await mediator.Send(new GetBookByIdQuery(id), cancellationToken);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody, Required] CreateBookCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody, Required] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}