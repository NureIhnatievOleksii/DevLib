using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.ArticleAggregate;
using Microsoft.Extensions.Logging;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public class CreateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IMapper mapper, ILogger<CreateDirectoryCommandHandler> logger)
    : IRequestHandler<CreateDirectoryCommand>
{
    public async Task Handle(CreateDirectoryCommand command, CancellationToken cancellationToken)
    {
        string DirectoryImgLink = "";

        if (command.File != null && command.File.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{command.File.FileName}";
            var filePath = Path.Combine("wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.File.CopyToAsync(stream);
            }

            DirectoryImgLink = $"/images/{fileName}" ;
        }

        var directory = new DLDirectory
        {
            DirectoryName = command.DirectoryName,
            ImgLink = DirectoryImgLink
        };

        await directoryRepository.CreateAsync(directory, cancellationToken);
    }
}
