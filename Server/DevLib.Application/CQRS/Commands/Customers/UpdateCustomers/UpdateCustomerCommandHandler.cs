using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper) 
    : IRequestHandler<UpdateCustomerCommand>
{
    public async Task Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(command.Id, cancellationToken)
                       ?? throw new Exception("Customer not found");

        mapper.Map(command, customer);

        await customerRepository.UpdateAsync(customer, cancellationToken);
    }
}
