using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.CustomerAggregate;

namespace DevLib.Application.CQRS.Commands.Customers.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper) 
    : IRequestHandler<CreateCustomerCommand>
{
    public async Task Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = mapper.Map<Customer>(command);

        await repository.CreateAsync(customer, cancellationToken);
    }
}
