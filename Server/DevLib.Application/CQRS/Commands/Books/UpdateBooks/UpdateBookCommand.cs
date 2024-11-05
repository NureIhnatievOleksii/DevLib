using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public record UpdateBookCommand
(
    Guid BookId,
    string? BookName,
    string? Author,
    IFormFile? BookImg,
    IFormFile? BookPdf
) : IRequest<IdentityResult>;