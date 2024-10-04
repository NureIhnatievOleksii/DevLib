using MediatR;

namespace DevLib.Application.CQRS.Commands.Customers.CreateCustomer;

public record CreateCustomerCommand
(
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
