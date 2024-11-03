using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public record CreateDirectoryCommand(
    string DirectoryName,
    IFormFile File
) : IRequest<string>;
