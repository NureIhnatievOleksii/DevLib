using MediatR;

namespace DevLib.Application.CQRS.Commands.Notes.DeleteNote;

public record DeleteNoteCommand(Guid Id) : IRequest;
