namespace DevLib.Application.CQRS.Dtos.Queries;

public record GetCustomerByIdQueryDto
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
);
