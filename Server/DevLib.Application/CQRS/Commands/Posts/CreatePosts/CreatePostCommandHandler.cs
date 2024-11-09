using AutoMapper;
using DevLib.Application.CQRS.Commands.Posts.CreatePosts;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Domain.PostAggregate;
using DevLib.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class CreatePostCommandHandler(IPostRepository postRepository, UserManager<User> userManager, IMapper mapper)
    : IRequestHandler<CreatePostCommand, string>
{
    public async Task<string> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(command.userId.ToString())
                           ?? throw new Exception("User not found");

            var post = new Post
            {
                Title = command.postName,
                Text = command.text,
                UserId = command.userId,
                DateTime = DateTime.UtcNow,
                User = user
            };

            await postRepository.CreateAsync(post, cancellationToken);

            return post.PostId.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}
