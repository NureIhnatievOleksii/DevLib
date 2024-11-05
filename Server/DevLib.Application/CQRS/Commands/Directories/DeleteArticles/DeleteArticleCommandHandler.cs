using MediatR;
using DevLib.Application.Interfaces.Repositories;

namespace DevLib.Application.CQRS.Commands.Directories.DeleteArticles;

public class DeleteArticleCommandHandler(IArticleRepository articleRepository)
    : IRequestHandler<DeleteArticleCommand>
{
    public async Task Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
    {
        var customer = await articleRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new Exception("Customer not found");

        await articleRepository.DeleteAsync(customer, cancellationToken);
    }
}
