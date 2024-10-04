using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Customers.GetAllCustomers
{
    public record GetAllCustomersQuery : IRequest<IReadOnlyList<GetAllCustomersQueryDto>>;
}
