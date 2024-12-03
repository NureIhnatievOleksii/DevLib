using DevLib.Application.CQRS.Dtos.Queries;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Admins;
public record GetAllUsersQuery : IRequest<List<GetAllUsersQueryDto>>;

