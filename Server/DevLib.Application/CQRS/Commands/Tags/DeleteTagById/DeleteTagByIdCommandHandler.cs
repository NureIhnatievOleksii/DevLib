using MediatR;
using DevLib.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Tags.DeleteTagById
{
    public class DeleteTagByIdCommandHandler : IRequestHandler<DeleteTagByIdCommand>
    {
        private readonly ITagRepository _tagRepository;

        public DeleteTagByIdCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(DeleteTagByIdCommand command, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetTagByIdAsync(command.TagId, cancellationToken);
            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            await _tagRepository.DeleteTagAsync(tag, cancellationToken);
        }
    }
}
