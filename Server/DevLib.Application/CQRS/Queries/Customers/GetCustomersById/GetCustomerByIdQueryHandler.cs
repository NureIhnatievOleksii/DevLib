using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdQueryDto>
{
    public async Task<GetCustomerByIdQueryDto> Handle(
        GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(query.Id, cancellationToken)
                       ?? throw new Exception("Customer not found");

        return mapper.Map<GetCustomerByIdQueryDto>(customer);
    }
}
