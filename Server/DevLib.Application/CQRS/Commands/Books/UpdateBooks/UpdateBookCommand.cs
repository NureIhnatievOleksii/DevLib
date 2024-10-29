using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Books.UpdateBook;

public record UpdateBookCommand
(
    Guid BookId,
    string BookName,
    string Author,
    IFormFile BookImg,
    IFormFile BookPdf,
    DateTime PublicationDateTime,
    List<TagUpdateDto> Tags
) : IRequest;

// todo oc delete dto
public record TagUpdateDto(
    Guid? TagId,
    string TagText
);