﻿using AutoMapper;
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
    private readonly ICommentRepository commentRepository;
    private readonly UserManager<DevLib.Domain.UserAggregate.User> userManager;
    private readonly IMapper mapper;

    public GetPostByIdQueryHandler(IPostRepository postRepository, ICommentRepository commentRepository, UserManager<DevLib.Domain.UserAggregate.User> userManager, IMapper mapper)
    {
        this.postRepository = postRepository;
        this.userManager = userManager;
        this.mapper = mapper;
        this.commentRepository = commentRepository;
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
                UserId: c.User.Id,
                AuthorName: c.User.UserName,
                AuthorImg: c.User.Photo,
                DateTime: c.DateTime,
                Text: c.Content,
                CommentId: c.CommentId,
                Comments: new List<CommentDto>()  
            )).ToList();

            foreach (var comment in commentDtos)
            {
                await commentRepository.GetReplies(comment,cancellationToken);
            }

            var resultPost = new GetPostByIdQueryDto
            (
                PostName: post.Title,
                Text: post.Text,
                DateTime: DateTime.UtcNow,
                AuthorName: user.UserName,
                AuthorImg: user.Photo,
                CommentsQuantity: comments.Count,
                PostId: post.PostId,
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

