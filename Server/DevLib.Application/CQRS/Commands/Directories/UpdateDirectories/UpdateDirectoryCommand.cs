using MediatR;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public record UpdateDirectoryCommand(Guid DirectoryId, string DirectoryName, string DirectoryImgUrl, List<ArticleUpdateDto> Articles) : IRequest;


// todo oc delete dto
public record ArticleUpdateDto(
    Guid? ArticleId, 
    string Name,
    string Text
);