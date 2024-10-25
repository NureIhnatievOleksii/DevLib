﻿using DevLib.Domain.ArticleAggregate;

namespace DevLib.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task CreateAsync(Article article, CancellationToken cancellationToken = default);
    }
}
