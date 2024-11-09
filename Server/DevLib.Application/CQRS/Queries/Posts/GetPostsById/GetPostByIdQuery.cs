using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Posts.GetPostsById;

public record GetPostByIdQuery(Guid postId) : IRequest<GetPostByIdQueryDto>;
