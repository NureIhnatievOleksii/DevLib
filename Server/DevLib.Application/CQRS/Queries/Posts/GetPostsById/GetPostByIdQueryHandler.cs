using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace DevLib.Application.CQRS.Queries.Posts.GetPostsById;

// todo fix shorten path name <DevLib.Domain.UserAggregate.User>
public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdQueryDto>
{
    private readonly IPostRepository postRepository;
    private readonly UserManager<DevLib.Domain.UserAggregate.User> userManager;
    private readonly IMapper mapper;

    public GetPostByIdQueryHandler(IPostRepository postRepository, UserManager<DevLib.Domain.UserAggregate.User> userManager, IMapper mapper)
    {
        this.postRepository = postRepository;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<GetPostByIdQueryDto> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var post = await postRepository.GetByIdAsync(query.postId, cancellationToken)
                           ?? throw new Exception("Post not found");

            var user = await userManager.FindByIdAsync(post.UserId.ToString())
                           ?? throw new Exception("User not found");

            var comments = await postRepository.GetCommentsByPostIdAsync(post.PostId, cancellationToken);

            var commentDtos = comments.Select(c => new CommentDto(
                AuthorName: c.User.UserName,
                AuthorImg: c.User.Photo,
                DateTime: c.DateTime,
                Text: c.Content,
                Comments: null  
            )).ToList();

            var resultPost = new GetPostByIdQueryDto
            (
                PostName: post.Title,
                Text: post.Text,
                DateTime: DateTime.UtcNow,
                AuthorName: user.UserName,
                AuthorImg: user.Photo,
                CommentsQuantity: comments.Count,
                Comments: commentDtos  
            );

            return resultPost;
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}

