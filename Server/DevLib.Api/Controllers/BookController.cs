using DevLib.Application.CQRS.Commands.Books.CreateBooks;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using DevLib.Application.CQRS.Queries.Books.GetBookById;
using DevLib.Application.CQRS.Queries.Books.LastPublishedBooks;
using DevLib.Application.CQRS.Queries.Books.SearchBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public async Task<IActionResult> CreateBook([FromForm, Required] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var allowedImageExtensions = new[] { ".jpg", ".png", ".jpeg" };
            var allowedFileExtensions = new[] { ".pdf", ".epub" };

            if (command.BookImg != null)
            {
                var imgExtension = Path.GetExtension(command.BookImg.FileName).ToLower();
                if (!allowedImageExtensions.Contains(imgExtension))
                {
                    return BadRequest(new { Message = "Invalid file extension. Allowed extensions are:  .jpg, .png, .jpeg" });
                }
            }

            if (command.BookPdf != null)
            {
                var fileExtension = Path.GetExtension(command.BookPdf.FileName).ToLower();
                if (!allowedFileExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { Message = "Invalid file extension. Allowed extensions are: .pdf, .epub" });
                }
            }

            var result = await mediator.Send(command, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Book was succesfully added" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("update-book")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromForm, Required] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var allowedImageExtensions = new[] { ".jpg", ".png", ".jpeg" };
            var allowedFileExtensions = new[] { ".pdf", ".epub" };

            if (command.BookImg != null)
            {
                var imgExtension = Path.GetExtension(command.BookImg.FileName).ToLower();
                if (!allowedImageExtensions.Contains(imgExtension))
                {
                    return BadRequest(new { Message = "Invalid file extension. Allowed extensions are:  .jpg, .png, .jpeg" });
                }
            }

            if (command.BookPdf != null)
            {
                var fileExtension = Path.GetExtension(command.BookPdf.FileName).ToLower();
                if (!allowedFileExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { Message = "ÍInvalid file extension. Allowed extensions are: .pdf, .epub" });
                }
            }

            var result = await mediator.Send(command, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Book was succesfulle updated" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("search-books")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? bookName, CancellationToken cancellationToken)
        {
            var books = await mediator.Send(new SearchBooksQuery(bookName ?? ""), cancellationToken);
            return Ok(books);
        }

        [HttpGet("last-published-books")]
        public async Task<IActionResult> GetLastPublishedBooks(CancellationToken cancellationToken)
        {
            var books = await mediator.Send(new LastPublishedBooksQuery(), cancellationToken);
            return Ok(books);
        }

    }
}