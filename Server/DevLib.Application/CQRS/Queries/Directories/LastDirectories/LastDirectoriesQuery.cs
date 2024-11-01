using MediatR;
using System.Collections.Generic;

namespace DevLib.Application.CQRS.Queries.Directories
{
    public record LastDirectoriesQuery() : IRequest<List<LastDirectoryDto>>;
}

public record LastDirectoryDto
{
    public Guid DirectoryId { get; init; }
    public string DirectoryName { get; init; }
    public string ImgLink { get; init; }
}
