using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Directories.GetDirectoryById
{
    public record GetDirectoryByIdQuery(Guid DirectoryId) : IRequest<GetDirectoryDto>;
}
