using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Notes.DeleteNote;

public class DeleteNoteCommandHandler(INoteRepository noteRepository)
    : IRequestHandler<DeleteNoteCommand>
{
    public async Task Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var customer = await noteRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new Exception("Customer not found");

        await noteRepository.DeleteAsync(customer, cancellationToken);
    }
}
