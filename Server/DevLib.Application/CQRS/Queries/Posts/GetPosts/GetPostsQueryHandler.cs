using AutoMapper;
using MediatR;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using DevLib.Domain.UserAggregate;

namespace DevLib.Application.CQRS.Queries.Posts.GetPosts;

// todo fix shorten path name <DevLib.Domain.UserAggregate.User>
public class GetPostsHandler(IPostRepository postRepository, UserManager<DevLib.Domain.UserAggregate.User> userManager, IMapper mapper)
    : IRequestHandler<GetPostsQuery, List<GetPostsQueryDto>?>
{
    public async Task<List<GetPostsQueryDto>?> Handle(
        GetPostsQuery query, CancellationToken cancellationToken)
    {
        try 
        { 
            var posts = await postRepository.GetAllAsync(cancellationToken)
                           ?? throw new Exception("Posts not found");

            var resultPosts = new List<GetPostsQueryDto>();

            foreach (var post in posts)
            {
                var user = await userManager.FindByIdAsync(post.UserId.ToString())
                           ?? throw new Exception($"User with ID {post.UserId} not found");

                resultPosts.Add(
                    new GetPostsQueryDto
                    (
                        PostName: post.Title,
                        DateTime: post.DateTime,
                        AuthorName: user.UserName,
                        AuthorImg: user.Photo,
                        PostId: post.PostId,
                        CommentsQuantity: 0
                    )
                ); 
            }
            
            return resultPosts;
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}
