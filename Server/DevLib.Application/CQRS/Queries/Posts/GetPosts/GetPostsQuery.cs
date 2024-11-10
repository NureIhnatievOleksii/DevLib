using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Posts.GetPosts;

public record GetPostsQuery() : IRequest<List<GetPostsQueryDto>?>;
