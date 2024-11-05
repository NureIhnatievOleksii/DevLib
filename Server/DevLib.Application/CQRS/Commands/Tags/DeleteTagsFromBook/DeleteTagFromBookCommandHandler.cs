using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.CQRS.Commands.Tags.DeleteTagsFromBook;

namespace DevLib.Application.CQRS.Commands.Customers.DeleteCustomer;

public class DeleteTagFromBookCommandHandler(ITagRepository tagRepositoryy)
    : IRequestHandler<DeleteTagFromBookCommand>
{
    public async Task Handle(DeleteTagFromBookCommand command, CancellationToken cancellationToken)
    {
        await tagRepositoryy.RemoveTagConnectionAsync(command.bookId, command.tagId, cancellationToken);
    }
}
