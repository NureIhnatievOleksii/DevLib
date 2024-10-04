using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Customers.GetAllCustomers;

public class GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<GetAllCustomersQueryDto>>
{
    public async Task<IReadOnlyList<GetAllCustomersQueryDto>> Handle(
        GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var allCustomers = await customerRepository.GetAllAsync(cancellationToken);

        return allCustomers
            .Select(x => mapper.Map<GetAllCustomersQueryDto>(x))
            .ToList();
    }
}
