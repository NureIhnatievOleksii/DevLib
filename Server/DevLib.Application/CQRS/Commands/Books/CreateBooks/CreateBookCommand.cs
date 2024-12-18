﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Books.CreateBooks;

public record CreateBookCommand
(
    string BookName,
    string Author,
    IFormFile BookImg,
    IFormFile BookPdf,
    string Tag
) : IRequest<IdentityResult>; 