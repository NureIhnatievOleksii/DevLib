using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Directories.UpdateArticles;

public record UpdateArticleCommand(
    Guid ArticleId,
    string? ArticleName,
    string? ArticleContent
) : IRequest;
