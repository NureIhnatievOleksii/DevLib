using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.ArticleAggregate;
using Microsoft.Extensions.Logging;

namespace DevLib.Application.CQRS.Commands.Directories.CreateDirectories;

public class CreateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IMapper mapper)
    : IRequestHandler<CreateDirectoryCommand, string>
{
    public async Task<string> Handle(CreateDirectoryCommand command, CancellationToken cancellationToken)
    {
        string directoryImgLink = "";

        if (command.File != null && command.File.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{command.File.FileName}";
            var imagesFolderPath = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.File.CopyToAsync(stream);
            }

            directoryImgLink = Path.Combine("images", fileName).Replace("\\", "/");
        }

        var directory = new DLDirectory
        {
            DirectoryName = command.DirectoryName,
            ImgLink = directoryImgLink
        };

        await directoryRepository.CreateAsync(directory, cancellationToken);

        return  directory.DirectoryId.ToString();
    }
}

