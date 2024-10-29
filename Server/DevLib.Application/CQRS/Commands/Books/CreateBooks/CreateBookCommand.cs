using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public record CreateBookCommand
(
    string BookName,
    string Author,
    IFormFile BookImg,
    // todo rename FilePath property
    string FilePath,
    List<TagCreateDto> Tags
) : IRequest;

// todo oc delete dto
public record TagCreateDto(
    string TagText
);