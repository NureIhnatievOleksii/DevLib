using MediatR;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public record CreateDirectoryCommand(string DirectoryName, string DirectoryImgUrl, List<ArticleDto> Articles) : IRequest;

// todo oc delete dto
public record ArticleDto(
    string Name,
    string Text
);