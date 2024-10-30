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
        public async Task<IActionResult> CreateBook([FromForm, Required] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var allowedImageExtensions = new[] { ".jpg", ".png", ".jpeg" };
            var allowedFileExtensions = new[] { ".pdf", ".epub" };

            if (command.BookImg != null)
            {
                var imgExtension = Path.GetExtension(command.BookImg.FileName).ToLower();
                if (!allowedImageExtensions.Contains(imgExtension))
                {
                    return BadRequest(new { Message = "Недопустиме розширення зображення. Доступні розширення: .jpg, .png, .jpeg" });
                }
            }

            if (command.FilePath != null)
            {
                var fileExtension = Path.GetExtension(command.FilePath.FileName).ToLower();
                if (!allowedFileExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { Message = "Недопустиме розширення файлу. Доступні розширення: .pdf, .epub" });
                }
            }

            var result = await mediator.Send(command, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Книгу було успішно додано." });
            }

            return BadRequest(result.Errors);
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