using MediatR;

namespace DevLib.Application.CQRS.Commands.Customers.DeleteCustomer;
 
public record DeleteCustomerCommand(Guid Id) : IRequest;
