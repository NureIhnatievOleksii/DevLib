namespace DevLib.Application.CQRS.Dtos.Queries;

public record GetAllCustomersQueryDto
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
