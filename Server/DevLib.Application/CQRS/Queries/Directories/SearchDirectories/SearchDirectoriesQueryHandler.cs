using AutoMapper;
using DevLib.Application.Interfaces.Repositories;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Directories.SearchDirectories
{
    public class SearchDirectoriesQueryHandler : IRequestHandler<SearchDirectoriesQuery, List<DirectoryDto>>
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IMapper _mapper;

        public SearchDirectoriesQueryHandler(IDirectoryRepository directoryRepository, IMapper mapper)
        {
            _directoryRepository = directoryRepository;
            _mapper = mapper;
        }

        public async Task<List<DirectoryDto>> Handle(SearchDirectoriesQuery query, CancellationToken cancellationToken)
        {
            var directories = await _directoryRepository.GetAllAsync(cancellationToken);

            // Если directoryName пустой, возвращаем все директории
            if (string.IsNullOrWhiteSpace(query.DirectoryName))
            {
                return _mapper.Map<List<DirectoryDto>>(directories);
            }

            // Фильтруем по совпадению имен директории
            var filteredDirectories = directories
                .Where(d => d.DirectoryName.Contains(query.DirectoryName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.Map<List<DirectoryDto>>(filteredDirectories);
        }
    }

}
