using MediatR;

namespace DevLib.Application.CQRS.Commands.Tags.UpdateTags;
public record UpdateTagCommand
(
    Guid TagId,
    string Name
) : IRequest;
