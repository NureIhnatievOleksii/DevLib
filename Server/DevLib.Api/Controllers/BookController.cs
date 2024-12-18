using DevLib.Application.CQRS.Commands.Books.CreateBooks;
using DevLib.Application.CQRS.Commands.Books.DeleteBookById;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using DevLib.Application.CQRS.Commands.Tags.DeleteTagsFromBook;
using DevLib.Application.CQRS.Queries.Books.GetBookById;
using DevLib.Application.CQRS.Queries.Books.LastPublishedBooks;
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
        [HttpGet("get-book/{bookId:guid}")]
        public async Task<IActionResult> GetBookById(Guid bookId, CancellationToken cancellationToken)
        {
            var query = new GetBookByIdQuery(bookId);
            var book = await mediator.Send(query, cancellationToken);
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
                    return BadRequest(new { Message = "Invalid file extension. Allowed extensions are: .pdf, .epub" });
                }
            }

            var result = await mediator.Send(command, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Book was succesfulle updated" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? tag, [FromQuery] string? bookName, CancellationToken cancellationToken)
        {
            try
            {
                var books = await mediator.Send(new SearchBooksQuery(tag, bookName ?? ""), cancellationToken);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("last-published-books")]
        public async Task<IActionResult> GetLastPublishedBooks(CancellationToken cancellationToken)
        {
            var books = await mediator.Send(new LastPublishedBooksQuery(), cancellationToken);
            return Ok(books);
        }

        [HttpDelete("remove-tag")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveTagFromBook([FromBody] DeleteTagFromBookCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("delete-book/{bookId}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> DeleteBookById(Guid bookId, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(new DeleteBookByIdCommand(bookId), cancellationToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}