using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Posts.DeletePosts;

public class DeletePostCommandHandler(IPostRepository postRepository)
    : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var post = await postRepository.GetByIdAsync(command.postId, cancellationToken)
                ?? throw new Exception("Post not found");

            await postRepository.DeleteAsync(post, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}");
        }
    }
}
       