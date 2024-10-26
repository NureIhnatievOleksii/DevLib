using MediatR;
using System.Collections.Generic;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public record UpdateDirectoryCommand(
    Guid DirectoryId,
    string DirectoryName,
    string DirectoryImgUrl,
    List<(Guid? ArticleId, string Name, string Text)>? Articles = null
) : IRequest;
