using AutoMapper;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Queries.Notes
{
    public class GetNotesByBookAndUserQueryHandler : IRequestHandler<GetNotesByBookAndUserQuery, List<GetNotesByBookAndUserQueryDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNotesByBookAndUserQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<List<GetNotesByBookAndUserQueryDto>> Handle(GetNotesByBookAndUserQuery query, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.GetNotesByBookAndUserAsync(query.BookId, query.UserId, cancellationToken);

            return notes.Select(note => new GetNotesByBookAndUserQueryDto
            {
                NoteId = note.NoteId,
                Note = note.Text,
                Type = note.Type 
            }).ToList();

        }
    }
}
