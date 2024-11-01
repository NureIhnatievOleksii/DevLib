using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Directories.SearchDirectories
{
    public record SearchDirectoriesQuery(string? DirectoryName) : IRequest<List<DirectoryDto>>;

}
public record DirectoryDto
{
    public Guid DirectoryId { get; init; }
    public string DirectoryName { get; init; }
    public string DirectoryImg { get; init; }
}