using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Posts.CreatePosts;
public record CreatePostCommand(
    string postName,
    string text,
    IFormFile? File,
    Guid userId
) : IRequest<string>;
