using MediatR;
using System;

namespace DevLib.Application.CQRS.Commands.Tags.DeleteTagById
{
    public record DeleteTagByIdCommand(Guid TagId) : IRequest;
}
