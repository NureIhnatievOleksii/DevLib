using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Customers.DeleteCustomer;

public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository) 
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new Exception("Customer not found");

        await customerRepository.DeleteAsync(customer, cancellationToken);
    }
}
