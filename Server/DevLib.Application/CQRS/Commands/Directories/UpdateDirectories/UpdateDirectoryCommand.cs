using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public record UpdateDirectoryCommand(
    Guid DirectoryId,
    string? DirectoryName,
    IFormFile? File
) : IRequest;