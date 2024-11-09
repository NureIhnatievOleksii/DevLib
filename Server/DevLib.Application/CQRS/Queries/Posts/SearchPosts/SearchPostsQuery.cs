using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.Posts.SearchPosts;

public record SearchPostsQuery(string postName) : IRequest<List<GetPostsQueryDto>?>;
