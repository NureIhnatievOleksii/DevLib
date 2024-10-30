using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevLib.Application.CQRS.Commands.Directories.CreateArticles;

public record CreateArticleCommand(
string ArticleName,
string ArticleContent,
Guid DirectoryId
) : IRequest;
