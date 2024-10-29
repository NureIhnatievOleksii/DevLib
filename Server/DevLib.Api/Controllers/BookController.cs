using DevLib.Application.CQRS.Commands.Books.CreateBooks;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using DevLib.Application.CQRS.Queries.Books.GetBookById;
using DevLib.Application.CQRS.Queries.Books.SearchBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevLib.Api.Controllers
{
    [Route("api/book")]
    public class BookController(IMediator mediator) : ControllerBase
    {
        [HttpGet("get-book/{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId, CancellationToken cancellationToken)
        {
            var book = await mediator.Send(new GetBookByIdQuery(bookId), cancellationToken);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [HttpPost("add-book")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook([FromBody, Required] CreateBookCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("update-book")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromBody, Required] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }


        [HttpGet("search-books/{BookName}")]
        public async Task<IActionResult> SearchBooks(string bookName, CancellationToken cancellationToken)
        {
            var books = await mediator.Send(new SearchBooksQuery(bookName), cancellationToken);
            return Ok(books);
        }
    }
}