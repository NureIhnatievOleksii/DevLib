using MediatR;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public record UpdateDirectoryCommand(
    string DirectoryName,
    string DirectoryImgUrl,
    List<ArticleUpdateDto>? Articles = null) : IRequest
{
    public Guid DirectoryId { get; init; }
}

// todo oc delete dto
public record ArticleUpdateDto(
    Guid? ArticleId, 
    string Name,
    string Text
);