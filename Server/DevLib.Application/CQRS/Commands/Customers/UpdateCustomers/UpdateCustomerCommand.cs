using MediatR;

namespace DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
public record UpdateCustomerCommand
(
    Guid Id,
    string Name,
    string Address,
    string Phone,
    string Email,
    string LinkedInUrl,
    string FacebookUrl,
    string InstagramUrl,
    string TwitterUrl,
    string WebsiteUrl
) : IRequest;
