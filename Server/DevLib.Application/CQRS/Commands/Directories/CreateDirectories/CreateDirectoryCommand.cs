using MediatR;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public record CreateDirectoryCommand(
    string DirectoryName, 
    string DirectoryImgUrl, 
    List<ArticleCreateDto> Articles
) : IRequest;

// todo oc delete dto
public record ArticleCreateDto(
    string Name,
    string Text
);