using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.TagAggregate;
using MediatR;

namespace DevLib.Application.CQRS.Commands.Tags.CreateTags;

public class CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    : IRequestHandler<CreateTagCommand>
{
    public async Task Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        Tag tag = mapper.Map<Tag>(command);

        await tagRepository.AddTagAsync(tag, cancellationToken);
    }
}