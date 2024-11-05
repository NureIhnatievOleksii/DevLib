using MediatR;

namespace DevLib.Application.CQRS.Commands.Directories.DeleteArticles;

public record DeleteArticleCommand(Guid Id) : IRequest;
