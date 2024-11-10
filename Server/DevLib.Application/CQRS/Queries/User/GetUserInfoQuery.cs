using DevLib.Application.CQRS.Dtos.Queries;
using MediatR;

namespace DevLib.Application.CQRS.Queries.User
{
   

    public record GetUserInfoQuery(Guid UserId) : IRequest<GetUserInfoQueryDto>;

}
