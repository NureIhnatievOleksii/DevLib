using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Customers.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<GetCustomerByIdQueryDto>;
}
