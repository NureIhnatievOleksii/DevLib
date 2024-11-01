using AutoMapper;
using DevLib.Application.CQRS.Queries.Directories;
using DevLib.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace DevLib.Application.CQRS.Commands.Directories.LastDirectories;

public class LastDirectoriesQueryHandler : IRequestHandler<LastDirectoriesQuery, List<LastDirectoryDto>>
{
    private readonly IDirectoryRepository _directoryRepository;
    private readonly IMapper _mapper;

    public LastDirectoriesQueryHandler(IDirectoryRepository directoryRepository, IMapper mapper)
    {
        _directoryRepository = directoryRepository;
        _mapper = mapper;
    }

    public async Task<List<LastDirectoryDto>> Handle(LastDirectoriesQuery request, CancellationToken cancellationToken)
    {
        var directories = await _directoryRepository.GetAllAsync(cancellationToken);

        var lastDirectories = directories
            .OrderByDescending(d => d.DirectoryId)
            .Take(Math.Min(8, directories.Count()))  // Возвращаем меньшее значение между 8 и фактическим количеством директорий
            .ToList();

        return _mapper.Map<List<LastDirectoryDto>>(lastDirectories);
    }
}
