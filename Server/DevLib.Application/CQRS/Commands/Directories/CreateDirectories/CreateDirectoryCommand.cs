using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public record CreateDirectoryCommand(
    string DirectoryName,
    string DirectoryImgUrl,
    List<ArticleCreateDto> Articles,
    IFormFile? File
) : IRequest;

public record ArticleCreateDto(
    string Name,
    string Text
);
