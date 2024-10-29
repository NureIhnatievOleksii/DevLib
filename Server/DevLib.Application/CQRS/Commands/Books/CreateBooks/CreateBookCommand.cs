using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public record CreateBookCommand
(
    string BookName,
    string Author,
    IFormFile BookImg,
    IFormFile BookPdf,
    DateTime PublicationDateTime,
    List<TagCreateDto> Tags
) : IRequest;

// todo oc delete dto
public record TagCreateDto(
    string TagText
);