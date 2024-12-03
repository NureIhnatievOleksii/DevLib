using MediatR;
using DevLib.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace DevLib.Application.CQRS.Commands.Directories.DeleteDirectory;

public class DeleteDirectoryCommandHandler(IDirectoryRepository directoryRepository)
    : IRequestHandler<DeleteDirectoryCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(DeleteDirectoryCommand command, CancellationToken cancellationToken)
    {
        var directory = (directoryRepository.GetByIdAsync(command.Id, cancellationToken)).Result;

        if(directory == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Such directory doesn`t exist" });
        }

        await directoryRepository.DeleteAsync(directory, cancellationToken);

        return IdentityResult.Success;

    }
}
