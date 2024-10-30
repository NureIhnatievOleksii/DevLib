using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.Exceptions;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;

public class UpdateDirectoryCommandHandler(IDirectoryRepository directoryRepository, IArticleRepository articleRepository, IMapper mapper) : IRequestHandler<UpdateDirectoryCommand>
{
    public async Task Handle(UpdateDirectoryCommand command, CancellationToken cancellationToken)
    {
        var directory = await directoryRepository.GetByIdAsync(command.DirectoryId, cancellationToken);
        if (directory == null)
            throw new NotFoundException($"Directory with ID {command.DirectoryId} not found.");

        string oldImgPath = directory.ImgLink != null
            ? Path.Combine("wwwroot", directory.ImgLink.TrimStart('/'))
            : null;
        string newImgLink = "";
        if (command.File != null && command.File.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{command.File.FileName}";
            var filePath = Path.Combine("wwwroot/images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.File.CopyToAsync(stream);
            }

            newImgLink = $"/images/{fileName}";

            if (!string.IsNullOrWhiteSpace(oldImgPath) && File.Exists(oldImgPath))
            {
                File.Delete(oldImgPath);
            }
        }
        if (!string.IsNullOrWhiteSpace(command.DirectoryName))
        {
            directory.DirectoryName = command.DirectoryName;
        }

        if (!string.IsNullOrWhiteSpace(newImgLink))
        {
            directory.ImgLink = newImgLink;
        }

        await directoryRepository.UpdateAsync(directory, cancellationToken);
    }
}
