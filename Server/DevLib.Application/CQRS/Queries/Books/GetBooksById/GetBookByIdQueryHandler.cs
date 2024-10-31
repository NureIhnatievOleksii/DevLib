using AutoMapper;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using MediatR;

namespace DevLib.Application.CQRS.Queries.Books.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetBookByIdQueryDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<GetBookByIdQueryDto> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            // Получаем книгу по ID
            var book = await _bookRepository.GetByIdAsync(query.Id, cancellationToken)
                       ?? throw new Exception("Book not found");

            // Получаем среднюю оценку книги
            var ratings = await _bookRepository.GetRatingsByBookIdAsync(query.Id, cancellationToken);
            var averageRating = ratings.Any() ? ratings.Average(r => r.PointsQuantity) : 0;

            // Получаем комментарии и составляем отзывы
            var comments = await _bookRepository.GetCommentsByBookIdAsync(query.Id, cancellationToken);
            var reviews = comments.Select(comment => new ReviewDto
            {
                UserImg = comment.User?.Photo ?? string.Empty,
                Rate = ratings.FirstOrDefault(r => r.UserId == comment.UserId)?.PointsQuantity ?? 0,
                CreationDate = comment.DateTime,
                Text = comment.Content
            }).ToList();

            // Заполняем DTO данными
            var bookDto = _mapper.Map<GetBookByIdQueryDto>(book);
            bookDto.AverageRating = averageRating;
            bookDto.Reviews = reviews;

            return bookDto;
        }
    }
}
