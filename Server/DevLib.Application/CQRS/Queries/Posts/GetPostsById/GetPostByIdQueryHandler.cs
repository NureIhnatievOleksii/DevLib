using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace DevLib.Application.CQRS.Queries.Posts.GetPostsById;

public class GetPostByIdQueryHandler(IPostRepository postRepository, UserManager<User> userManager, IMapper mapper)
    : IRequestHandler<GetPostByIdQuery, GetPostByIdQueryDto>
{
    public async Task<GetPostByIdQueryDto> Handle(
        GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        try 
        { 
            var post = await postRepository.GetByIdAsync(query.postId, cancellationToken)
                           ?? throw new Exception("Post not found");

            var user = await userManager.FindByIdAsync(post.UserId.ToString())
                           ?? throw new Exception("User not found");

            var resultPost = new GetPostByIdQueryDto
            (
                PostName: post.Title,
                Text: post.Text,
                DateTime: DateTime.UtcNow,
                AuthorName: user.UserName,
                AuthorImg: user.Photo,
                CommentsQuantity: 0,
                Comments: null
            );

            return resultPost;
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}
